using IndigoEngine.Agents;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace TestProject
{
    
    
    /// <summary>
    ///Это класс теста для StateTest, в котором должны
    ///находиться все модульные тесты StateTest
    ///</summary>
    [TestClass()]
    public class StateTest
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
        ///Тест для Norm
        ///</summary>
        [TestMethod()]
        public void NormTest()
        {
            State target = new StateLiving(); // TODO: инициализация подходящего значения
            double[] actual = new double[10];

            actual[0] = target.Norm();

            target.FindByName("Health").CurrentUnitValue = 80;

            actual[1] = target.Norm();

            target.FindByName("Health").CurrentUnitValue = 80;
            target.FindByName("Stamina").CurrentUnitValue = 80;
            target.FindByName("FoodSatiety").CurrentUnitValue = 80;
            target.FindByName("WaterSatiety").CurrentUnitValue = 80;
            target.FindByName("Peacefulness").CurrentUnitValue = 80;

            actual[2] = target.Norm();

            target.FindByName("Health").CurrentUnitValue = 21;
            target.FindByName("Stamina").CurrentUnitValue = 80;
            target.FindByName("FoodSatiety").CurrentUnitValue = 80;
            target.FindByName("WaterSatiety").CurrentUnitValue = 80;
            target.FindByName("Peacefulness").CurrentUnitValue = 80;

            actual[3] = target.Norm();

            target.FindByName("Health").CurrentUnitValue = 19;
            target.FindByName("Stamina").CurrentUnitValue = 80;
            target.FindByName("FoodSatiety").CurrentUnitValue = 80;
            target.FindByName("WaterSatiety").CurrentUnitValue = 80;
            target.FindByName("Peacefulness").CurrentUnitValue = 80;

            actual[4] = target.Norm();

            target.FindByName("Health").CurrentUnitValue = 0;
            target.FindByName("Stamina").CurrentUnitValue = 80;
            target.FindByName("FoodSatiety").CurrentUnitValue = 80;
            target.FindByName("WaterSatiety").CurrentUnitValue = 80;
            target.FindByName("Peacefulness").CurrentUnitValue = 80;

            actual[5] = target.Norm();

            target.FindByName("Health").CurrentUnitValue = 50;
            target.FindByName("Stamina").CurrentUnitValue = 50;
            target.FindByName("FoodSatiety").CurrentUnitValue = 50;
            target.FindByName("WaterSatiety").CurrentUnitValue = 50;
            target.FindByName("Peacefulness").CurrentUnitValue = 10;

            actual[6] = target.Norm();

            target.FindByName("Health").CurrentUnitValue = 100;
            target.FindByName("Stamina").CurrentUnitValue = 100;
            target.FindByName("FoodSatiety").CurrentUnitValue = 100;
            target.FindByName("WaterSatiety").CurrentUnitValue = 100;
            target.FindByName("Peacefulness").CurrentUnitValue = 10;

            actual[7] = target.Norm();

            target.FindByName("Health").CurrentUnitValue = 50;
            target.FindByName("Stamina").CurrentUnitValue = 50;
            target.FindByName("FoodSatiety").CurrentUnitValue = 50;
            target.FindByName("WaterSatiety").CurrentUnitValue = 50;
            target.FindByName("Peacefulness").CurrentUnitValue = 30;

            actual[8] = target.Norm();

            Console.WriteLine("Norm = {0}", actual[0]);

            Assert.IsTrue(actual[0] >= 0 && actual[0] <= 1);
        }
    }
}
