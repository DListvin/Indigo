using IndigoEngine;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Threading;
using IndigoEngine.Agents;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace TestProject
{
    
    
    /// <summary>
    ///Это класс теста для ModelTest, в котором должны
    ///находиться все модульные тесты ModelTest
    ///</summary>
    [TestClass()]
    public class ModelTest
    {


        private TestContext testContextInstance;

        /// <summary>
        ///Получает или устанавливает контекст теста, в котором предоставляются
        ///сведения о текущем тестовом запуске и обеспечивается его функциональность.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Дополнительные атрибуты теста
        // 
        //При написании тестов можно использовать следующие дополнительные атрибуты:
        //
        //ClassInitialize используется для выполнения кода до запуска первого теста в классе
        //[ClassInitialize()]
        //public static void MyClassInitialize(TestContext testContext)
        //{
        //}
        //
        //ClassCleanup используется для выполнения кода после завершения работы всех тестов в классе
        //[ClassCleanup()]
        //public static void MyClassCleanup()
        //{
        //}
        //
        //TestInitialize используется для выполнения кода перед запуском каждого теста
        //[TestInitialize()]
        //public void MyTestInitialize()
        //{
        //}
        //
        //TestCleanup используется для выполнения кода после завершения каждого теста
        //[TestCleanup()]
        //public void MyTestCleanup()
        //{
        //}
        //
        #endregion


        /// <summary>
        ///Timing test
        ///</summary>
        [TestMethod()]
        public void Test1()
        {
            DateTime start;
            DateTime stop;
            TimeSpan delta;
            int ms;
            long ticks;
            int sec;

            Model target = new Model();

            start = DateTime.Now;
            for (int i = 0; i < 500000; ++i)
                target.AddAgent(new AgentLivingIndigo());
            #region Time calculation
            stop = DateTime.Now;
            delta = stop - start;
            ticks = delta.Ticks;
            ms = delta.Milliseconds;
            sec = delta.Seconds;
            Console.WriteLine("Time was {0} ticks or {1} s {2} ms", ticks, sec, ms);
            Assert.IsTrue(true);
            #endregion

            int a = target.GetAgentsAmount();
            Console.WriteLine(a);

            for (int i = 0; i < 1; ++i)
            {                    
                target.StepNIterationsForward();
            }
            
            
        }

        /// <summary>
        ///Serialization test
        ///</summary>
        [TestMethod()]
        public void SerializationTest()
        {
            Model original = new Model();

            FileStream sstream = new FileStream("ModelStateTest.dat", FileMode.Create);
            BinaryFormatter sformatter = new BinaryFormatter();
            try
            {
                sformatter.Serialize(sstream, original);
            }
            catch (SerializationException e)
            {
                Console.WriteLine("Failed to save. Reason: " + e.Message);
                throw;
            }
            sstream.Close();
            Console.WriteLine("Model saved!");

            Model loaded;

            FileStream lstream = new FileStream("ModelStateTest.dat", FileMode.Open);
            BinaryFormatter lformatter = new BinaryFormatter();
            try
            {
                loaded = (Model)lformatter.Deserialize(lstream);
            }
            catch (SerializationException e)
            {
                Console.WriteLine("Failed to load. Reason: " + e.Message);
                throw;
            }
            lstream.Close();
            Console.WriteLine("Model loaded!");
            
            Assert.IsTrue(original.Equals(loaded), "We loaded something distinuished from what we saved!");
            
        }
    }
}
