using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using IndigoEngine.Agents;
using IndigoEngine.ActionsNew;
using NLog;

namespace IndigoEngine
{
    /// <summary>
    /// World - logical moments in model.
    /// </summary>
    [Serializable]
    public class World : IWorldToAgent
    {
		private static Logger logger = LogManager.GetCurrentClassLogger();

        public delegate void worldCommand();  //Delegate to store some commands for world recompute

        private List<Agent> agents;               //List of all agents in the world
        private List<ActionAbstract> actions;     //List of all actions, that must be performed (refreshing each loop iteration)
        private List<GlobalInstruction> worldGlobalInstructions; // List of Instructions, to add or Delete agents at right place in MainLoop

		#region Constructors

        public World()
        {
            Initialise();
        }
	
		#endregion

		#region Properties

        public List<Agent> Agents
        {
            get { return agents; }
			set { agents = value; }
        }

        public List<ActionAbstract> Actions
        {
            get { return actions; }
			set { actions = value; }
        }

		#endregion

		/// <summary>
		/// Loking for agent at coordinates
		/// </summary>
		/// <param name="argAgentLoc">agent location</param>
		/// <returns>Found agent or null</returns>
		public Agent GetAgentAt(Point argAgentLoc)
		{
			logger.Debug("Finding agent at {0}", argAgentLoc);
			return agents.FirstOrDefault(ag => 
			{
				return ag.CurrentLocation.Coords == argAgentLoc;
			});
		}

		#region Map generating

			/// <summary>
			/// Generate the forest
			/// </summary>
			/// <param name="argForestCenter">Center of the forest</param>
			/// <param name="argForestSize">Forest size. Defines the square to border the forest</param>
			/// <param name="argDensity">Forest density (0 - 1). Defines trees density in the forest square</param>
			public void GenerateForest(Point argForestCenter, Size argForestSize, double argDensity)
			{
				logger.Debug("Generating new forest at {0} of size {1} with {2} density", argForestCenter, argForestSize, argDensity);

				int chanceForNewTree = 50;                         //Chance for algorythm to create a new tree near the current       
				Random currentChance = new Random();               //Current counted chance to compare with chanceForNewTree   

				List<Point> newTrees = new List<Point>();          //Trees, that created in the current phase
				List<Point> currentTreesArray = new List<Point>(); //Trees, that are using for creating new ones

				int borderLeft = argForestCenter.X - argForestSize.Width / 2;    //Left border of the forest
				int borderRight = argForestCenter.X + argForestSize.Width / 2;   //Right border of the forest  
				int borderTop = argForestCenter.Y - argForestSize.Height / 2;    //Top border of the forest   
				int borderBottom = argForestCenter.Y + argForestSize.Height / 2; //Bottom border of the forest     

				int maxTrees = (int)(argForestSize.Height * argForestSize.Width * argDensity);  //Max number of trees in the forest

				int totalTreesAdded = 0;    //Total new trees created

				currentTreesArray.Add(argForestCenter);
			
				while(currentTreesArray.Count != 0 && totalTreesAdded < maxTrees)
				{
					foreach(Point p in currentTreesArray)
					{	
						for(int i = p.X - 1; i <= p.X + 1; i++)
						{
							if(i > borderRight || i < borderLeft)
							{
								continue;
							}
							for(int j = p.Y - 1; j <= p.Y + 1; j++)
							{	
								if(totalTreesAdded > maxTrees / 10) //edges of the forest are more discontinuous, so we are not checking diagonal cells
								{
									if(j != p.Y && (i == p.X - 1 || i == p.X + 1))
									{
										continue;
									}
									if(i != p.X && (j == p.Y - 1 || j == p.Y + 1))
									{
										continue;
									}
								}
								if(j > borderBottom || j < borderTop)
								{
									continue;
								}
								if(i == p.X && j == p.Y)
								{
									continue;
								}
								if(currentChance.Next(100) < chanceForNewTree)
								{
									if(GetAgentAt(new Point(i, j)) != null)
									{
										continue;
									}
                                    if (GetAgentAt(new Point(i, j)) == null)
                                    {
                                        AgentTree currentAddingAgent = new AgentTree();
                                        currentAddingAgent.Name = "Tree_at_" + i.ToString() + "_" + j.ToString();
                                        currentAddingAgent.CurrentLocation = new Location(i, j);
                                        AddAgent(currentAddingAgent);
                                        newTrees.Add(new Point(i, j));
                                        totalTreesAdded++;
                                    }
								}
							}
						}
					}
					currentTreesArray.Clear();
					currentTreesArray.AddRange(newTrees);
					newTrees.Clear();
				}

				logger.Info("Generated new forest. {0} trees created", totalTreesAdded);
			}

		#endregion

		#region World management

			/// <summary>
			/// Here is the main loop
			/// </summary>
			public void MainLoopIteration()
			{    
				UpdateAgentFeelings();

				actions.Clear();
                worldGlobalInstructions.Clear();

				foreach (Agent agent in Agents)
				{
					actions.Add( agent.Decide());
				}

				//SolveActionConflicts();

                foreach (ActionAbstract action in Actions)
                {

                    worldGlobalInstructions.AddRange( action.CompleteRegular());
                }
				foreach (Agent agent in Agents)
				{
					agent.StateRecompute();
				}

                lock (Agents) //No other thread can access Agenst while this thread in within this block
                {
                    foreach (GlobalInstruction GI in worldGlobalInstructions)
                    {
                        PerformGlobalInstrution(GI);
                    }
                }
			}

            /// <summary>
            /// Performing of one global instruction
            /// </summary>
            /// <param name="GI">Global Instruction</param>
            private void PerformGlobalInstrution(GlobalInstruction GI)
            {
                if (GI.worldOperation == OperationWorld.addAgent)
                    AddAgent(GI.TargetAgent);
                if (GI.worldOperation == OperationWorld.deleteAgent)
                    DeleteAgent(GI.TargetAgent);
                if (GI.worldOperation == OperationWorld.DverMneZapili) // kill agent and add corpse of agent
                {
                    Func<Agent> corpseFunc;   //Func to get corpse of the deleting agent
                    Agent corpse;             //Corpse of the deleting agent
                    if (WorldRules.CorpseDictionary.TryGetValue(GI.TargetAgent.GetType(), out corpseFunc))
                    {
                        corpse = corpseFunc();
                        corpse.CurrentLocation = GI.TargetAgent.CurrentLocation;
                        corpse.Name = GI.TargetAgent.Name + "_corpse";
                        AddAgent(corpse);
                    }
                    GI.TargetAgent.Inventory.DropAll();
                    DeleteAgent(GI.TargetAgent);
                }
            }

			/// <summary>
			/// Here we basicly create world.
			/// </summary>
			public void Initialise()
			{
                ActionsNew.Actions.Init();
				Agent currentAddingAgent; //Agent, that is adding to the world now. This variable is necessary to configure the agent

				Agents = new List<Agent>();
				Actions = new List<ActionAbstract>();
                worldGlobalInstructions = new List<GlobalInstruction>();

				/*//Terrific test init (for hard and cruel test)
            for (int i = 0; i < 3; ++i)
				{
					currentAddingAgent = new AgentLivingIndigo();
					currentAddingAgent.Name = string.Format("Indigo{0}",i);
					currentAddingAgent.Location = new Point((i+1)*10, (i+1)*10);
					currentAddingAgent.CurrentState.Health.MaxValue = 100;
					currentAddingAgent.CurrentState.Health.CurrentUnitValue = 100;
					(currentAddingAgent as AgentLivingIndigo).AgentsRangeOfView = 100;
					currentAddingAgent.Inventory.StorageSize = 3;
					AddAgent(currentAddingAgent);	
				}*/


            //Basic test init
				currentAddingAgent = new AgentLivingIndigo();
				currentAddingAgent.Name = "Indigo1";
				currentAddingAgent.CurrentLocation = new Location(10, 10);
				currentAddingAgent.CurrentState.Health.MaxValue = 100;
				currentAddingAgent.CurrentState.Health.CurrentUnitValue = 100;
				(currentAddingAgent as AgentLivingIndigo).AgentsRangeOfView = 20;
				currentAddingAgent.Inventory.StorageSize = 3;
				AddAgent(currentAddingAgent);			
			
				currentAddingAgent = new AgentLivingIndigo();
				currentAddingAgent.Name = "Indigo2";
				currentAddingAgent.CurrentLocation = new Location(10, 15);
				currentAddingAgent.CurrentState.Health.MaxValue = 100;
				currentAddingAgent.CurrentState.Health.CurrentUnitValue = 100;
				(currentAddingAgent as AgentLivingIndigo).AgentsRangeOfView = 20;
				currentAddingAgent.Inventory.StorageSize = 3;
				AddAgent(currentAddingAgent);
			
				currentAddingAgent = new AgentLivingIndigo();
				currentAddingAgent.Name = "Indigo3";
				currentAddingAgent.CurrentLocation = new Location(15, 15);
				currentAddingAgent.CurrentState.Health.MaxValue = 100;
				currentAddingAgent.CurrentState.Health.CurrentUnitValue = 100;
				(currentAddingAgent as AgentLivingIndigo).AgentsRangeOfView = 20;
				currentAddingAgent.Inventory.StorageSize = 3;
				AddAgent(currentAddingAgent);

                currentAddingAgent = new AgentLivingIndigo();
                currentAddingAgent.Name = "Indigo4";
                currentAddingAgent.CurrentLocation = new Location(-20, 0);
                currentAddingAgent.CurrentState.Health.MaxValue = 100;
                currentAddingAgent.CurrentState.Health.CurrentUnitValue = 100;
                (currentAddingAgent as AgentLivingIndigo).AgentsRangeOfView = 10;
                currentAddingAgent.Inventory.StorageSize = 3;
                AddAgent(currentAddingAgent);

				currentAddingAgent = new AgentItemFoodFruit();
				currentAddingAgent.Name = "Fruit1";
				currentAddingAgent.CurrentLocation = new Location(0, 2);
				currentAddingAgent.CurrentState.Health.MaxValue = 100;
				currentAddingAgent.CurrentState.Health.CurrentUnitValue = 100;
				AddAgent(currentAddingAgent);

				currentAddingAgent = new AgentItemFoodFruit();
				currentAddingAgent.Name = "Fruit2";
				currentAddingAgent.CurrentLocation = new Location(2, 0);
				currentAddingAgent.CurrentState.Health.MaxValue = 100;
				currentAddingAgent.CurrentState.Health.CurrentUnitValue = 100;
				AddAgent(currentAddingAgent);

				currentAddingAgent = new AgentPuddle();
				currentAddingAgent.Name = "Puddle1";
				currentAddingAgent.CurrentLocation = new Location(-2, -2);
				currentAddingAgent.CurrentState.Health.MaxValue = 200;
				currentAddingAgent.CurrentState.Health.CurrentUnitValue = 200;
				AddAgent(currentAddingAgent);

                currentAddingAgent = new AgentPuddle();
                currentAddingAgent.Name = "Puddle2";
                currentAddingAgent.CurrentLocation = new Location(-15, 0);
                currentAddingAgent.CurrentState.Health.MaxValue = 200;
                currentAddingAgent.CurrentState.Health.CurrentUnitValue = 200;
                AddAgent(currentAddingAgent);

                currentAddingAgent = new AgentPuddle();
                currentAddingAgent.Name = "Puddle3";
                currentAddingAgent.CurrentLocation = new Location(-10, 16);
                currentAddingAgent.CurrentState.Health.MaxValue = 200;
                currentAddingAgent.CurrentState.Health.CurrentUnitValue = 200;
                AddAgent(currentAddingAgent);

				currentAddingAgent = new AgentItemResLog();
				currentAddingAgent.Name = "Log1";
				AddAgent(currentAddingAgent);

				currentAddingAgent = new AgentItemResLog();
				currentAddingAgent.Name = "Log2";
				currentAddingAgent.CurrentLocation = new Location(3, 4);
				AddAgent(currentAddingAgent);

				currentAddingAgent = new AgentItemResLog();
				currentAddingAgent.Name = "Log3";
				currentAddingAgent.CurrentLocation = new Location(-3, 4);
				AddAgent(currentAddingAgent);

				currentAddingAgent = new AgentItemResLog();
				currentAddingAgent.Name = "Log4";
				currentAddingAgent.CurrentLocation = new Location(-3, -4);
				AddAgent(currentAddingAgent);

				currentAddingAgent = new AgentTree();
				currentAddingAgent.Name = "Tree_at_" + 1.ToString() + "_" + 1.ToString();
				currentAddingAgent.CurrentLocation = new Location(1, 1);
				AddAgent(currentAddingAgent);

                GenerateForest(new Point(-5, -5), new Size(10, 10), 0.50);
                GenerateForest(new Point(-15, 5), new Size(20, 20), 0.20);
                GenerateForest(new Point(-30, 5), new Size(50, 50), 0.03);
			}
		
			/// <summary>
			/// Here world somehow decides how to slove conflicts
			/// </summary>
			private void SolveActionConflicts()
			{
				//kuyvkhguvkty
				//     ^
				//     |
				//We shouldn't delete this, VERY IMPORTANT!
				//TODO: Confirm, if it's so.
				//Will be deleted 06.02.2015
			}

			/// <summary>
			/// Clears agents FieldOfView and than fills it with agents and action within range of view
			/// </summary>
			private void UpdateAgentFeelings()
			{
				lock(Agents)
				{
					foreach (Agent agent in Agents)
					{
						agent.CurrentVision.CurrentView.Clear();
                
						if (!agent.CurrentLocation.HasOwner)
						{
							//Adding agents
							agent.CurrentVision.CurrentView.AddRange(agents.Where(a =>
							{
								return !a.CurrentLocation.HasOwner && a != agent && Agent.Distance(a, agent) < agent.AgentsRangeOfView;
							}));
							//Adding actions
							/*agent.CurrentVision.CurrentView.AddRange(actions.Where(a =>
							{
								return !a. && Agent.Distance(agent, a.Subject) < agent.AgentsRangeOfView;
							}));*/
						}
					}
				}
			}
			
			/// <summary>
			/// Deleting agent from the world completely. Nobody should use any other ways of deleting some agent, exept this method
			/// </summary>
			/// <param name="argAgentToDelete">Agent to delete</param>
			public void DeleteAgent(Agent argAgentToDelete)
			{
				if(argAgentToDelete.CurrentLocation.HasOwner)
				{
					argAgentToDelete.CurrentLocation.Owner.Inventory.PopAgent(argAgentToDelete);
				}
				agents.Remove(argAgentToDelete);
			}

			/// <summary>
			/// Adding agent to the world. Nobody should use any other ways of adding some agent, exept this method
			/// </summary>
			/// <param name="argAgentToAdd">Agent to add</param>
			public void AddAgent(Agent argAgentToAdd)
			{
				argAgentToAdd.HomeWorld = this;
				Agents.Add(argAgentToAdd);
			}

		#endregion

        #region IWorldToAgent realisation

            public Exception AskWorldForAction(ActionAbstract action)
            {
                /*action.World = this;
				if(action.CheckForLegitimacy() != null) //Checking action for legitimacy
				{
					return action.CheckForLegitimacy();
				}
				if(Actions.Any(act =>  //Checking action for conflicts
				{
					if(action.GetType() != act.GetType()) //Comparing actions types
					{
						return false;
					}
					if(action.CompareTo(act) == 0)  //Comparing actions for conflicts
					{
						return true;
					}
					return false;
				}))
				{	
					return new ConflictException();
				}*/
				Actions.Add(action);

				return null;
           
            }
			
			public bool AskWorldForEuthanasia(Agent sender)
			{
                if (!agents.Contains(sender))  //Checking existing of the agent to delete
				{
                    return false;
				}
                worldGlobalInstructions.Add(new GlobalInstruction(sender, OperationWorld.DverMneZapili));
				return true;
			}

        #endregion

        #region ObjectMethodsOverride



        #endregion
    }
}
