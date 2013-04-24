using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IndigoEngine.Agents;
using System.Drawing;
using NLog;

namespace IndigoEngine.Actions
{
	/// <summary>
	/// Class for converting action type to current action
	/// </summary>
	public static class ActionsManager
	{		
		private static Logger logger = LogManager.GetCurrentClassLogger();

		//Dictionary for different binary actions realisations for different participants
		#region Dictionary itself. Dangerous! Do not open! 

		private static Dictionary<Type, Dictionary<Type, Dictionary<Type, Func<Agent, Agent, object[], ActionAbstract>>>> BinaryActionsDataBase = new Dictionary<Type, Dictionary<Type, Dictionary<Type, Func<Agent, Agent, object[], ActionAbstract>>>>()
		{
			//Action attack dictionary
			{
				typeof(ActionAttack),
				new Dictionary<Type, Dictionary<Type, Func<Agent, Agent, object[], ActionAbstract>>>()
				{
					{
						typeof(AgentLivingIndigo), 
						new Dictionary<Type, Func<Agent, Agent, object[], ActionAbstract>>()
						{
							{
								typeof(AgentLivingIndigo),
								(sub, ob, par) => {return new ActionAttack(sub, ob, par);}
							}
						}
					}
				}
			},	
			//Action drink dictionary
			{
				typeof(ActionDrink),
				new Dictionary<Type, Dictionary<Type, Func<Agent, Agent, object[], ActionAbstract>>>()
				{
					{
						typeof(AgentLivingIndigo), 
						new Dictionary<Type, Func<Agent, Agent, object[], ActionAbstract>>()
						{
							{
								typeof(AgentPuddle),
								(sub, ob, par) => {return new ActionDrink(sub, ob);}
							}
						}
					}
				}
			},	
			//Action eat dictionary
			{
				typeof(ActionEat),
				new Dictionary<Type, Dictionary<Type, Func<Agent, Agent, object[], ActionAbstract>>>()
				{
					{
						typeof(AgentLivingIndigo), 
						new Dictionary<Type, Func<Agent, Agent, object[], ActionAbstract>>()
						{
							{
								typeof(AgentItemFoodFruit),
								(sub, ob, par) => {return new ActionEat(sub, ob);}
							}
						}
					}
				}
			},	
			//Action obtain fruit dictionary
			{
				typeof(ActionObtainFood),
				new Dictionary<Type, Dictionary<Type, Func<Agent, Agent, object[], ActionAbstract>>>()
				{
					{
						typeof(AgentLivingIndigo), 
						new Dictionary<Type, Func<Agent, Agent, object[], ActionAbstract>>()
						{
							{
								typeof(AgentTree),
								(sub, ob, par) => {return new ActionObtainFoodFruit(sub, ob);}
							},							
							{
								typeof(AgentItemFoodFruit),
								(sub, ob, par) => {return new ActionObtainFoodFruit(sub, ob);}
							}
						}
					}
				}
			},	
			//Action obtain log dictionary
			{
				typeof(ActionObtainRes),
				new Dictionary<Type, Dictionary<Type, Func<Agent, Agent, object[], ActionAbstract>>>()
				{
					{
						typeof(AgentLivingIndigo), 
						new Dictionary<Type, Func<Agent, Agent, object[], ActionAbstract>>()
						{
							{
								typeof(AgentTree),
								(sub, ob, par) => {return new ActionObtainResLog(sub, ob);}
							},
							{
								typeof(AgentItemResLog),
								(sub, ob, par) => {return new ActionObtainResLog(sub, ob);}
							}
						}
					}
				}
			},	
			//Action rest dictionary
			{
				typeof(ActionRest),
				new Dictionary<Type, Dictionary<Type, Func<Agent, Agent, object[], ActionAbstract>>>()
				{
					{
						typeof(AgentLivingIndigo), 
						new Dictionary<Type, Func<Agent, Agent, object[], ActionAbstract>>()
						{
							{
								typeof(AgentManMadeShelterCamp),
								(sub, ob, par) => {return new ActionRest(sub, ob);}
							},
						}
					}
				}
			},	
		};

		#endregion
		
		//Dictionary for different actions witout objects realisations for different participants
		#region Dictionary itself. Dangerous! Do not open! 
		private static Dictionary<Type, Dictionary<Type, Func<Agent, object[], ActionAbstract>>> UnaryActionsDataBase = new Dictionary<Type, Dictionary<Type, Func<Agent, object[], ActionAbstract>>>()
		{
			//Action break shelter dictionary
			{
				typeof(ActionBuildShelter),
				new Dictionary<Type, Func<Agent, object[], ActionAbstract>>()
				{
					{
						typeof(AgentLivingIndigo), 
						(sub, par) => {return new ActionBuildShelterCamp(sub, par);}
					}
				}
			},
			//Action go dictionary
			{
				typeof(ActionGo),
				new Dictionary<Type, Func<Agent, object[], ActionAbstract>>()
				{
					{
						typeof(AgentLivingIndigo), 
						(sub, par) => {return new ActionGo(sub, par);}
					}
				}
			},
			//Action grow fruit dictionary
			{
				typeof(ActionGrowFruit),
				new Dictionary<Type, Func<Agent, object[], ActionAbstract>>()
				{
					{
						typeof(AgentTree), 
						(sub, par) => {return new ActionGrowFruit(sub);}
					}
				}
			},
			//Action do nothing dictionary
			{
				typeof(ActionDoNothing),
				new Dictionary<Type, Func<Agent, object[], ActionAbstract>>()
				{
					{
						typeof(AgentLivingIndigo), 
						(sub, par) => {return new ActionDoNothing(sub);}
					},
					{
						typeof(AgentManMadeShelterCamp), 
						(sub, par) => {return new ActionDoNothing(sub);}
					},
					{
						typeof(AgentItemFoodFruit), 
						(sub, par) => {return new ActionDoNothing(sub);}
					},
					{
						typeof(AgentItemResLog), 
						(sub, par) => {return new ActionDoNothing(sub);}
					},
					{
						typeof(AgentPuddle), 
						(sub, par) => {return new ActionDoNothing(sub);}
					},
					{
						typeof(AgentTree), 
						(sub, par) => {return new ActionDoNothing(sub);}
					}
				}
			},
            //Action wander dictionary
            {
                typeof(ActionWander),
                new Dictionary<Type, Func<Agent, object[], ActionAbstract>>()
				{
					{
						typeof(AgentLivingIndigo), 
						(sub, par) => {return new ActionWander(sub);}
					}
				}
            }

		};	
		#endregion

		/// <summary>
		/// Getting the action object for current participants and action type
		/// </summary>
		/// <param name="argActionType">Action type</param>
		/// <param name="argCurrentInfo">Action info (need for optimisation)</param>
		/// <param name="argSubject">Subject</param>
		/// <param name="argObject">Object</param>
		/// <param name="argParams">Action params</param>
		/// <returns>Action object</returns>
		public static ActionAbstract GetActionForCurrentParticipants(Type argActionType, ActionInfoAttribute argCurrentInfo, Agent argSubject, Agent argObject, params object[] argParams)
		{
			logger.Trace("Getting action for action type {0}, subject: {1}, object: {2}", argActionType, argSubject.Name, argObject != null ? argObject.Name : "none");

			if(!argActionType.IsSubclassOf(typeof(ActionAbstract)))
			{
				logger.Error("Trying to get action for none-action type: {0}!", argActionType);
				return null;
			}
			
			ActionAbstract result = null; //Action to return
			if(argCurrentInfo.RequiresObject)
			{
				Dictionary<Type, Dictionary<Type, Func<Agent, Agent, object[], ActionAbstract>>> subjectTypeDictionary; //supportive vars
				Dictionary<Type, Func<Agent, Agent, object[], ActionAbstract>> objectTypeDictionary; //supportive vars
				Func<Agent, Agent, object[], ActionAbstract> actionFunc; //supportive vars

				BinaryActionsDataBase.TryGetValue(argActionType, out subjectTypeDictionary);
				subjectTypeDictionary.TryGetValue(argSubject.GetType(), out objectTypeDictionary);
				objectTypeDictionary.TryGetValue(argObject.GetType(), out actionFunc);
				
				result = actionFunc(argSubject, argObject, argParams);			
			}
			else
			{
				Dictionary<Type, Func<Agent, object[], ActionAbstract>> subjectTypeDictionary; //supportive vars
				Func<Agent, object[], ActionAbstract> actionFunc; //supportive vars

				UnaryActionsDataBase.TryGetValue(argActionType, out subjectTypeDictionary);
				subjectTypeDictionary.TryGetValue(argSubject.GetType(), out actionFunc);

				result = actionFunc(argSubject, argParams);							
			}

			logger.Debug("Get action for {0}", argActionType);

			return result;
		}
	}
}
