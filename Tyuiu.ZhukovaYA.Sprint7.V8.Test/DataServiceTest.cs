using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using Tyuiu.ZhukovaYA.Sprint7.V8.Lib;

namespace Tyuiu.ZhukovaYA.Sprint7.V8.Test
{
    [TestClass]
    public class DataServiceTest
    {
        [TestMethod]
        public void TestFuelConsumptionPerDistanceTraveled()
        {
            Actions action = new Actions();
            Cars car = new Cars()
            {
                ID = 1,
                LicensePlate = "1",
                CarBrand = "1",
                TechnicalCondition = true,
                AverageSpeed = 1,
                LoadCapacity = 1,
                FuelConsumption = 20
            };
            double result = action.FuelConsumptionPerDistanceTraveled(car,200);
            Assert.AreEqual(result, 40);
        }

        [TestMethod]
        public void TestCheckingTheCar()
        {
            Actions action = new Actions();
            List<Cars> cars = new List<Cars>();
            cars.Add(new Cars
            {
                ID = 1,
                LicensePlate = "1",
                CarBrand = "1",
                TechnicalCondition = true,
                AverageSpeed = 1,
                LoadCapacity = 1,
                FuelConsumption = 20
            });
            cars.Add(new Cars
            {
                ID = 1,
                LicensePlate = "1",
                CarBrand = "1",
                TechnicalCondition = false,
                AverageSpeed = 1,
                LoadCapacity = 200,
                FuelConsumption = 20
            });

            List<Cars> result = action.CheckingTheCar(cars, 200);

            List<Cars> carsResult = new List<Cars>();
            CollectionAssert.AreEqual(result, carsResult);
        }
    }
}
