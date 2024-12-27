using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tyuiu.ZhukovaYA.Sprint7.V8.Lib
{
    [Serializable]
    public class Cars
    {
        //ID
        public int ID {  get; set; }
        //номерной знак
        public string LicensePlate { get; set; }
        //марка
        public string CarBrand { get; set; }
        //техническое состояние
        public bool TechnicalCondition { get; set; }
        //средняя скорость
        public double AverageSpeed { get; set; }
        //грузоподъемность
        public double LoadCapacity { get; set; }
        //расход топлива
        public double FuelConsumption { get; set; }

    }

    [Serializable]
    public class Drivers
    {
        //ID
        public int ID { get; set; }
        //табельный номер водителя
        public string ServiceNumber { get; set; }
        //ФИО
        public string FIO { get; set; }
        //дата рождения
        public DateTime DateOfBirth { get; set; }
        //стаж работы
        public int WorkExperience { get; set; }
        //оклад
        public double Salary { get; set; }
    }

    [Serializable]
    public class Transportation
    {
        //ID
        public int ID { get; set; }
        //
        public int CarsID { get; set; }
        //
        public int DriversID { get; set; }
        //дата выезда
        public DateTime DepartureDate { get; set; }
        //дата прибытия
        public DateTime ArrivalDate { get; set; }
        //место назначения
        public string Destination { get; set; }
        //расстояние
        public double Distance { get; set; }
        //расход горючего
        public double FuelConsumption { get; set; }
        //масса груза
        public double CargoWeight { get; set; }
    }

    public class Actions
    {
        public double FuelConsumptionPerDistanceTraveled(Cars car, double distance)
        {
            return (double)car.FuelConsumption*(distance/100);
        }

        public List<Cars> CheckingTheCar (List<Cars> cars, int WeightOfCargo)
        {
            return cars.Where(s => s.TechnicalCondition && s.LoadCapacity >= WeightOfCargo).ToList();
        }


        public List<Cars> DataOutputCars(string path)
        {
            try
            {
                var lines = File.ReadAllLines(path, System.Text.Encoding.Default);
                List<Cars> result = new List<Cars>();

                foreach (var line in lines)
                {
                    var fields = line.Split(';');
                    Cars car = new Cars()
                    {
                        ID = Convert.ToInt32(fields[0].Trim()),
                        LicensePlate = fields[1].Trim(),
                        CarBrand = fields[2].Trim(),
                        TechnicalCondition = Convert.ToBoolean(fields[3].Trim()),
                        AverageSpeed = Convert.ToDouble(fields[4].Trim()),
                        LoadCapacity = Convert.ToDouble(fields[5].Trim()),
                        FuelConsumption = Convert.ToDouble(fields[6].Trim())
                    };
                    result.Add(car);
                }
                return result;
            }
            catch
            {
                List<Cars> nullresult = new List<Cars>();
                return nullresult;
            }
        }

        public string DataEntryCars(List<Cars> cars, string path)
        {

            using (var sw = new StreamWriter(path, false, Encoding.Default))
            {
                for (int i = 0; i < cars.Count(); i++)
                {
                    sw.WriteLine($"{cars[i].ID};{cars[i].LicensePlate};{cars[i].CarBrand};{cars[i].TechnicalCondition};{cars[i].AverageSpeed};{cars[i].LoadCapacity};{cars[i].FuelConsumption}");
                }
            }

            return null;
        }

        public List<Drivers> DataOutputDrivers(string path)
        {
            try
            {
                var lines = File.ReadAllLines(path, System.Text.Encoding.Default);
                List<Drivers> result = new List<Drivers>();

                foreach (var line in lines)
                {
                    var fields = line.Split(';');
                    Drivers drivers = new Drivers()
                    {
                        ID = Convert.ToInt32(fields[0].Trim()),
                        ServiceNumber = fields[1].Trim(),
                        FIO = fields[2].Trim(),
                        DateOfBirth = Convert.ToDateTime(fields[3].Trim()),
                        WorkExperience = Convert.ToInt32(fields[4].Trim()),
                        Salary = Convert.ToDouble(fields[5].Trim()),
                    };
                    result.Add(drivers);
                }
                return result;
            }
            catch
            {
                List<Drivers> nullresult = new List<Drivers>();
                return nullresult;
            }
        }

        public string DataEntryDrivers(List<Drivers> drivers, string path)
        {

            using (var sw = new StreamWriter(path, false, Encoding.Default))
            {
                for (int i = 0; i < drivers.Count(); i++)
                {
                    sw.WriteLine($"{drivers[i].ID};{drivers[i].ServiceNumber};{drivers[i].FIO};{drivers[i].DateOfBirth};{drivers[i].WorkExperience};{drivers[i].Salary}");
                }
            }
            return null;
        }

        public List<Transportation> DataOutputTransportation(string path)
        {
            try
            {
                var lines = File.ReadAllLines(path, System.Text.Encoding.Default);
                List<Transportation> result = new List<Transportation>();

                foreach (var line in lines)
                {
                    var fields = line.Split(';');
                    Transportation transportation = new Transportation()
                    {
                        ID = Convert.ToInt32(fields[0].Trim()),
                        CarsID = Convert.ToInt32(fields[1].Trim()),
                        DriversID = Convert.ToInt32(fields[2].Trim()),
                        DepartureDate = Convert.ToDateTime(fields[3].Trim()),
                        ArrivalDate = Convert.ToDateTime(fields[4].Trim()),
                        Destination = fields[5].Trim(),
                        Distance = Convert.ToDouble(fields[6].Trim()),
                        FuelConsumption = Convert.ToDouble(fields[7].Trim()),
                        CargoWeight = Convert.ToDouble(fields[8].Trim()),
                    };
                    result.Add(transportation);
                }
                return result;
            }
            catch
            {
                List<Transportation> nullresult = new List<Transportation>();
                return nullresult;
            }
        }

        public string DataEntryTransportation(List<Transportation> transportation, string path)
        {

            using (var sw = new StreamWriter(path, false, Encoding.Default))
            {
                for (int i = 0; i < transportation.Count(); i++)
                {
                    sw.WriteLine($"{transportation[i].ID};{transportation[i].CarsID};{transportation[i].DriversID};{transportation[i].DepartureDate};{transportation[i].ArrivalDate};{transportation[i].Destination};{transportation[i].Distance};{transportation[i].FuelConsumption};{transportation[i].CargoWeight}");
                }
            }
            return null;
        }
    }
}
