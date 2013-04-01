using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IndigoEngine
{
    /// <summary>
    /// Интерфейс, реализуемый миром, используемый при работе с UI.
    /// </summary>
    interface IObservableWorld
    {
        IEnumerable<Agent> Agents {get;} //Получить список всех агентов в мире.
        //long ModelIterations { get; } //Получить прошедшее число итераций моделирования.
        //TimeSpan ModelIterationTick { get; set; } //Настроить промежуток времени между итерациями моделирования.

        void Initialise(); //Инициализирует мир (создаёт карту).
        void Start(); //Запускает главный цикл.
        void Stop(); //Приостанавливает гланый цикл.
    }

    /// <summary>
    /// Мир - сущность, контролирующая главный цикл, включающий в себя все игровые сущности.
    /// </summary>
    public class World : IObservableWorld
    {
        List<Agent> agents;
        long passedModelIterations;
        TimeSpan modelIterationTick;

        public IEnumerable<Agent> Agents
        {
            get
            {
                return agents;
            }
        }

        public void Initialise()
        {
            agents = new List<Agent>();
            passedModelIterations = 0;
            modelIterationTick = TimeSpan.FromSeconds(2);
        }

        public void Start()
        {

        }

        public void Stop()
        {

        }


    }
}
