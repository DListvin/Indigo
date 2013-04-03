using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using IndigoEngine.Agents;

namespace IndigoEngine
{
    /// <summary>
    /// Интерфейс, реализуемый моделью, используемый при работе с UI.
    /// </summary>
    public interface IObservableModel
    {
        IEnumerable<Agent> Agents {get;} //Получить список всех агентов в мире.
        ModelState State { get; } //Получить состояние модели
        long ModelIterations { get; } //Получить прошедшее число итераций моделирования.
        TimeSpan ModelIterationTick { get; set; } //Настроить промежуток времени между итерациями моделирования.

        void Initialise(); //Инициализирует мир (создаёт карту).
        void Start(); //Запускает главный цикл.
        void Pause(); //Приостанавливает гланый цикл.
        void Resume(); //Убрать с паузы
        void Stop(); //Выключить модель безопасным способом
    }
    
    /// <summary>
    /// Описывает возможные состояния модели
    /// </summary>
    public enum ModelState
    {
        Uninitialised,
        Initialised,
        Running,
        Paused,
        Stopping,
        Error
    }

    /// <summary>
    /// Модель - сущность, инкапсулирующая техническую обработку мира (работа с процессом). Имеет мир своим полем.
    /// </summary>
    public class Model : IObservableModel
    {
        #region Variables

        World world;
        long passedModelIterations;
        TimeSpan modelIterationTick;
        Thread thread; //Это переменная для контроля за выполнением модели в отдельном процессе
        ModelState state = ModelState.Uninitialised;

        #endregion

        #region Properties

        public long ModelIterations { get { return passedModelIterations; } } //Получить прошедшее число итераций моделирования.

        public TimeSpan ModelIterationTick { get { return modelIterationTick; } set { modelIterationTick = value; } } //Настроить промежуток времени между итерациями моделирования.

        public ModelState State { get { return state; } } //Получить состояние модели

        //Получить агентов из мира
        public IEnumerable<Agent> Agents
        {
            get
            {
                return world.Agents;
            }
        }

        #endregion

        #region Methods

        public Model()
        {
            Initialise();
        }        

        public void Initialise()
        {
            world = new World();
            passedModelIterations = 0;
            modelIterationTick = TimeSpan.FromSeconds(2);
            thread = new Thread(MainLoop);
            state = ModelState.Initialised;
        }
        
        public void Start()
        {
            if (state == ModelState.Initialised)
            {
                state = ModelState.Running;
                thread.Start(); //Запустить процесс
            }
        }

        public void Pause()
        {
            state = ModelState.Paused;
        }

        public void Resume()
        {
            state = ModelState.Running;
        }

        public void Stop()
        {
            state = ModelState.Stopping;
            thread.Join(); //Жду счастливого окончания процесса
            state = ModelState.Initialised;
        }

        public void MainLoop()
        {
            try
            {
                for (; ; )
                {
                    if (state == ModelState.Stopping)
                        break;
                    if (state == ModelState.Paused)
                        continue;
                    if (state == ModelState.Running)
                    {
                        //There out main loop is running
                        world.MainLoopIteration();

                        //Обрабатывам конец итерации
                        ++passedModelIterations;
                        Thread.Sleep(modelIterationTick);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                state = ModelState.Error;
            }
        }

        #endregion
    }
}
