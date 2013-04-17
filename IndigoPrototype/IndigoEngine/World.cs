using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using IndigoEngine.Agents;
using IndigoEngine.Actions;
using NLog;

namespace IndigoEngine
{
    /// <summary>
    /// World - logical moments in model.
    /// </summary>
    [Serializable]
    public class World : IWorldToAction, IWorldToAgent
    {
		private static Logger logger = LogManager.GetCurrentClassLogger();

        public delegate void worldCommand();  //Delegate to store some commands for world recompute

        private List<Agent> agents;               //List of all agents in the world
        private List<ActionAbstract> actions;     //List of all actions, that must be performed (refreshing each loop iteration)
        private List<worldCommand> worldCommands; // List of Delegates, to add or Delete agents at right place in MainLoop

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

				Actions.Clear();

				foreach (Agent agent in Agents)
				{
					agent.Decide();
				}

				//SolveActionConflicts();

				foreach (ActionAbstract action in Actions)
				{
					action.CalculateFeedbacks();
				}

				foreach (Agent agent in Agents)
				{
					agent.StateRecompute();
				}

				foreach (worldCommand com in worldCommands)
				{
					com.DynamicInvoke();
				}
				worldCommands.Clear();
			}

			/// <summary>
			/// Here we basicly create world.
			/// </summary>
			public void Initialise()
			{
				Agent currentAddingAgent; //Agent, that is adding to the world now. This variable is necessary to configure the agent

				Agents = new List<Agent>();
				Actions = new List<ActionAbstract>();
				worldCommands = new List<worldCommand>();

				//GenerateForest(new Point(-10, -10), new Size(10, 10), 0.50);

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
				(currentAddingAgent as AgentLivingIndigo).AgentsRangeOfView = 100;
				currentAddingAgent.Inventory.StorageSize = 3;
				AddAgent(currentAddingAgent);			
			
				currentAddingAgent = new AgentLivingIndigo();
				currentAddingAgent.Name = "Indigo2";
				currentAddingAgent.CurrentLocation = new Location(10, 15);
				currentAddingAgent.CurrentState.Health.MaxValue = 100;
				currentAddingAgent.CurrentState.Health.CurrentUnitValue = 100;
				(currentAddingAgent as AgentLivingIndigo).AgentsRangeOfView = 100;
				currentAddingAgent.Inventory.StorageSize = 3;
				AddAgent(currentAddingAgent);
			
				currentAddingAgent = new AgentLivingIndigo();
				currentAddingAgent.Name = "Indigo3";
				currentAddingAgent.CurrentLocation = new Location(15, 15);
				currentAddingAgent.CurrentState.Health.MaxValue = 100;
				currentAddingAgent.CurrentState.Health.CurrentUnitValue = 100;
				(currentAddingAgent as AgentLivingIndigo).AgentsRangeOfView = 100;
				currentAddingAgent.Inventory.StorageSize = 3;
				AddAgent(currentAddingAgent);

				currentAddingAgent = new AgentItemFruit();
				currentAddingAgent.Name = "Fruit1";
				currentAddingAgent.CurrentLocation = new Location(0, 2);
				currentAddingAgent.CurrentState.Health.MaxValue = 100;
				currentAddingAgent.CurrentState.Health.CurrentUnitValue = 100;
				AddAgent(currentAddingAgent);

				currentAddingAgent = new AgentItemFruit();
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

				currentAddingAgent = new AgentItemLog();
				currentAddingAgent.Name = "Log1";
				AddAgent(currentAddingAgent);

				currentAddingAgent = new AgentItemLog();
				currentAddingAgent.Name = "Log2";
				currentAddingAgent.CurrentLocation = new Location(3, 4);
				AddAgent(currentAddingAgent);

				currentAddingAgent = new AgentItemLog();
				currentAddingAgent.Name = "Log3";
				currentAddingAgent.CurrentLocation = new Location(-3, 4);
				AddAgent(currentAddingAgent);

				currentAddingAgent = new AgentItemLog();
				currentAddingAgent.Name = "Log4";
				currentAddingAgent.CurrentLocation = new Location(-3, -4);
				AddAgent(currentAddingAgent);

				currentAddingAgent = new AgentTree();
				currentAddingAgent.Name = "Tree_at_" + 1.ToString() + "_" + 1.ToString();
				currentAddingAgent.CurrentLocation = new Location(1, 1);
				AddAgent(currentAddingAgent);
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
						agent.CurrentVision.CurrentView.AddRange(actions.Where(a =>
						{
							return !a.Subject.CurrentLocation.HasOwner && Agent.Distance(agent, a.Subject) < agent.AgentsRangeOfView;
						}));
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

            public bool AskWorldForAction(ActionAbstract action)
            {
                action.World = this;
				if(action.CheckForLegitimacy()) //Checking action itself if it is legimate
				{
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
						return false;
					}
					Actions.Add(action);
					return true;
				}		
						
				return false;
            }
			
			public bool AskWorldForDeletion(Agent sender)
			{
                if (!agents.Contains(sender))  //Checking existing of the agent to delete
				{
                    return false;
				}
                worldCommands.Add(() =>
                {
					DeleteAgent(sender);
                });
				return true;
			}

        #endregion

        #region IWorldToAction realisation

            public bool AskWorldForDeletion(object sender, Agent obj)
            {
                if (sender.GetType().BaseType != typeof(ActionAbstract)) //Checking sender type (only actions accepted)
				{
                    return false;
				}
                if (!agents.Contains(obj))  //Checking existing of the agent to delete
				{
                    return false;
				}
                worldCommands.Add(() =>
                {
					DeleteAgent(obj);
                });
                return true;
            }

            public bool AskWorldForAddition(object sender, Agent obj)
            {
                if (sender.GetType().BaseType != typeof(ActionAbstract)) //Checking sender type (only actions accepted)
				{
                    return false;
				}
				if(!obj.CurrentLocation.HasOwner && GetAgentAt(obj.CurrentLocation.Coords) != null) //Checking for adding in the place of some agent
				{
					return false;
				}
                worldCommands.Add(() =>
                {
                    AddAgent(obj);
                });
                
                return true;
            }

        #endregion

        #region ObjectMethodsOverride



        #endregion
    }
}
