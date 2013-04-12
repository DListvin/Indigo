using IndigoEngine;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Threading;
using IndigoEngine.Agents;

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
        ///Model test 1
        ///</summary>
        [TestMethod()]
        public void Test1()
        {
            Model target = new Model();

            for (int i = 0; i < 4; ++i)
                target.AddAgent(new AgentLivingIndigo());

                for (int i = 0; i < 100; ++i)
                {
                    //TODO - measure time
                    target.StepNIterationsForward();
                }

            Console.WriteLine("write result");
            Assert.IsTrue(true);
        }

        /// <summary>
        ///Model test 2
        ///</summary>
        [TestMethod()]
        public void Test2()
        {
            Model target = new Model();

            for (int i = 0; i < 400; ++i)
                target.AddAgent(new AgentLivingIndigo());

            for (int i = 0; i < 100; ++i)
            {
                //TODO - measure time
                target.StepNIterationsForward();
            }

            Console.WriteLine("write result");
            Assert.IsTrue(true);
        }      
    }
}
