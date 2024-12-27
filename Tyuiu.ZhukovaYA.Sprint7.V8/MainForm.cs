using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using Tyuiu.ZhukovaYA.Sprint7.V8.Lib;

namespace Tyuiu.ZhukovaYA.Sprint7.V8
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();

            CreateTableCars();
            buttonEditCar_ZYA.Enabled = false;
            buttonDeleteCar_ZYA.Enabled =false;
            comboBoxFiltering_ZYA.SelectedIndex = 0;

            CreateTableDrivers();
            buttonDriversDelete_ZYA.Enabled = false;
            buttonDriversEdit_ZYA.Enabled = false;

            CreateTableTransportation();
            buttonTransportationDelete_ZYA.Enabled = false;
            buttonTransportationEdit_ZYA.Enabled = false;
            textBoxFuelConsumptionTransportation_ZYA.Enabled = false;

            comboBoxCarId_ZYA.DataSource = cars;
            comboBoxCarId_ZYA.DisplayMember = "LicensePlate";
            comboBoxCarId_ZYA.Enabled = false;

            comboBoxDriverId_ZYA.DataSource = drivers;
            comboBoxDriverId_ZYA.DisplayMember = "ServiceNumber";

        }
        Actions action = new Actions();

        //-------------------------------------------------------------------------------------------------------------------------------------------
        //Автомобили
        Cars EditCar;
        List<Cars> cars;
        string pathCars = Directory.GetCurrentDirectory() +"\\Cars.csv";

        private void CreateTableCars()
        {
            cars = action.DataOutputCars(pathCars);

            List<Cars> carsFilter = cars;
            switch (comboBoxFiltering_ZYA.SelectedIndex)
            {
                case 0: carsFilter = cars; break;
                case 1: carsFilter = carsFilter.Where(a => a.TechnicalCondition == true).ToList(); break;
                case 2: carsFilter = carsFilter.Where(a => a.TechnicalCondition == false).ToList(); break;
            }
            dataGridViewCars_ZYA.DataSource = carsFilter.ToArray();
            label.Text = "Кол-во машин: " + carsFilter.Count().ToString();

            //int colums = 6;
            //int rows = cars.Count;
            //dataGridViewCars_ZYA.ColumnCount = colums;
            //dataGridViewCars_ZYA.RowCount = rows;

            //dataGridViewCars_ZYA.Columns[0].HeaderCell.Value = "Номерной знак";
            //dataGridViewCars_ZYA.Columns[1].HeaderCell.Value = "Марка";
            //dataGridViewCars_ZYA.Columns[2].HeaderCell.Value = "Tехническое состояние";
            //dataGridViewCars_ZYA.Columns[3].HeaderCell.Value = "Cредняя скорость";
            //dataGridViewCars_ZYA.Columns[4].HeaderCell.Value = "Грузоподъемность";
            //dataGridViewCars_ZYA.Columns[5].HeaderCell.Value = "Расход топлива";

            //for (int i = 0; i < rows; i++)
            //{
            //    dataGridViewCars_ZYA.Rows[i].Cells[0].Value = cars[i].LicensePlate;
            //    dataGridViewCars_ZYA.Rows[i].Cells[1].Value = cars[i].CarBrand;
            //    if (cars[i].TechnicalCondition) dataGridViewCars_ZYA.Rows[i].Cells[2].Value = "Готов к работе";
            //    else dataGridViewCars_ZYA.Rows[i].Cells[2].Value = "Не готов к работе";
            //    dataGridViewCars_ZYA.Rows[i].Cells[3].Value = cars[i].AverageSpeed + " км/ч";
            //    dataGridViewCars_ZYA.Rows[i].Cells[4].Value = cars[i].LoadCapacity + " т.";
            //    dataGridViewCars_ZYA.Rows[i].Cells[5].Value = cars[i].FuelConsumption + " л/100 км";
            //}
        }


        private void buttonAddCar_ZYA_Click(object sender, EventArgs e)
        {
            try
            {
                int id = 0;
                if (cars.Count > 0) id = (int)cars.Last().ID + 1;
                cars.Add(new Cars()
                {
                    ID = id,
                    LicensePlate = textBoxLicensePlate_ZYA.Text,
                    CarBrand = textBoxCarBrand_ZYA.Text,
                    TechnicalCondition = checkBoxTechnicalCondition_ZYA.Checked,
                    AverageSpeed = Convert.ToDouble(textBoxAverageSpeed_ZYA.Text),
                    LoadCapacity = Convert.ToDouble(textBoxLoadCapacity_ZYA.Text),
                    FuelConsumption = Convert.ToDouble(textBoxFuelConsumption_ZYA.Text)
                });
                action.DataEntryCars(cars, pathCars);
                CreateTableCars();
            }
            catch
            {
                MessageBox.Show("Не верный ввод данных", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dataGridViewCars_ZYA_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            EditCar = (Cars)cars.Where(a => a.ID == Convert.ToInt32(dataGridViewCars_ZYA[0, e.RowIndex].Value.ToString())).First();
            try
            {
                buttonEditCar_ZYA.Enabled = true;
                buttonDeleteCar_ZYA.Enabled = true;
                textBoxAverageSpeed_ZYA.Text = EditCar.AverageSpeed.ToString();
                textBoxCarBrand_ZYA.Text = EditCar.CarBrand.ToString();
                textBoxFuelConsumption_ZYA.Text = EditCar.FuelConsumption.ToString();
                textBoxLicensePlate_ZYA.Text = EditCar.LicensePlate.ToString();
                textBoxLoadCapacity_ZYA.Text = EditCar.LoadCapacity.ToString();
                checkBoxTechnicalCondition_ZYA.Checked = EditCar.TechnicalCondition;
            }
            catch { }
        }

        private void buttonEditCar_ZYA_Click(object sender, EventArgs e)
        {
            try
            {
                EditCar.AverageSpeed = Convert.ToDouble(textBoxAverageSpeed_ZYA.Text);
                EditCar.CarBrand = textBoxCarBrand_ZYA.Text;
                EditCar.FuelConsumption = Convert.ToDouble(textBoxFuelConsumption_ZYA.Text);
                EditCar.LicensePlate = textBoxLicensePlate_ZYA.Text;
                EditCar.LoadCapacity = Convert.ToDouble(textBoxLoadCapacity_ZYA.Text);
                EditCar.TechnicalCondition = checkBoxTechnicalCondition_ZYA.Checked;
                action.DataEntryCars(cars, pathCars);
                CreateTableCars();
                buttonEditCar_ZYA.Enabled = false;
                buttonDeleteCar_ZYA.Enabled = false;
            }
            catch 
            {
                MessageBox.Show("Не верный ввод данных", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonDeleteCar_ZYA_Click(object sender, EventArgs e)
        {
            cars.Remove(EditCar);
            action.DataEntryCars(cars, pathCars);
            CreateTableCars();
            buttonEditCar_ZYA.Enabled = false;
            buttonDeleteCar_ZYA.Enabled = false;
        }

        private void checkBoxTechnicalCondition_ZYA_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxTechnicalCondition_ZYA.Checked) checkBoxTechnicalCondition_ZYA.Text = "Готов к работе";
            else checkBoxTechnicalCondition_ZYA.Text = "Не готов к работе";
        }

        private void comboBoxFiltering_ZYA_DropDownClosed(object sender, EventArgs e)
        {
            CreateTableCars();
        }

        //-------------------------------------------------------------------------------------------------------------------------------------------
        //Водители
        Drivers EditDriver;
        List<Drivers> drivers;
        // string pathDrivers = "C:\\Users\\Win\\source\\repos\\Tyuiu.ZhukovaYA.Sprint7\\Drivers.csv";
        string pathDrivers = Directory.GetCurrentDirectory() + "\\Drivers.csv";

        private void CreateTableDrivers()
        {
            drivers = action.DataOutputDrivers(pathDrivers);
            List<Drivers> driversFilter = drivers;
            driversFilter = driversFilter.Where(a => a.FIO.ToLower().Contains(textBoxSearch_ZYA.Text.ToLower()) || a.ServiceNumber.ToLower().Contains(textBoxSearch_ZYA.Text.ToLower())).ToList();
            dataGridViewDrivers_ZYA.DataSource = driversFilter.ToArray();
            comboBoxDriverId_ZYA.DataSource = drivers;
        }

        private void buttonDriversEdit_ZYA_Click(object sender, EventArgs e)
        {
            try
            {
                EditDriver.ServiceNumber = textBoxServiceNumber_ZYA.Text;
                EditDriver.FIO = textBoxFIO_ZYA.Text;
                EditDriver.DateOfBirth = Convert.ToDateTime(dateTimePickerDateOfBirth_ZYA.Text);
                EditDriver.WorkExperience = Convert.ToInt32(textBoxWorkExperience_ZYA.Text);
                EditDriver.Salary = Convert.ToDouble(textBoxSalary_ZYA.Text);
                action.DataEntryDrivers(drivers, pathDrivers);
                CreateTableDrivers();
                buttonDriversEdit_ZYA.Enabled = false;
                buttonDriversDelete_ZYA.Enabled = false;
            }
            catch
            {
                MessageBox.Show("Не верный ввод данных", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonDriversDelete_ZYA_Click(object sender, EventArgs e)
        {
            drivers.Remove(EditDriver);
            action.DataEntryDrivers(drivers, pathDrivers);
            CreateTableDrivers();
            buttonDriversEdit_ZYA.Enabled = false;
            buttonDriversDelete_ZYA.Enabled = false;
        }

        private void buttonDriversAdd_ZYA_Click(object sender, EventArgs e)
        {
            try
            {
                int id = 0;
                if (drivers.Count > 0) id = (int)drivers.Last().ID + 1;
                drivers.Add(new Drivers()
                {
                    ID = id,
                    ServiceNumber = textBoxServiceNumber_ZYA.Text,
                    FIO = textBoxFIO_ZYA.Text,
                    DateOfBirth = Convert.ToDateTime(dateTimePickerDateOfBirth_ZYA.Text),
                    WorkExperience = Convert.ToInt32(textBoxWorkExperience_ZYA.Text),
                    Salary = Convert.ToDouble(textBoxSalary_ZYA.Text),
                });
                action.DataEntryDrivers(drivers, pathDrivers);
                CreateTableDrivers();
            }
            catch
            {
                MessageBox.Show("Не верный ввод данных", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dataGridViewDrivers_ZYA_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            EditDriver = (Drivers)drivers.Where(a => a.ID == Convert.ToInt32(dataGridViewDrivers_ZYA[0, e.RowIndex].Value.ToString())).First();
            try
            {
                buttonDriversEdit_ZYA.Enabled = true;
                buttonDriversDelete_ZYA.Enabled = true;
                textBoxServiceNumber_ZYA.Text = EditDriver.ServiceNumber.ToString();
                textBoxFIO_ZYA.Text = EditDriver.FIO.ToString();
                dateTimePickerDateOfBirth_ZYA.Text = EditDriver.DateOfBirth.ToString();
                textBoxWorkExperience_ZYA.Text = EditDriver.WorkExperience.ToString();
                textBoxSalary_ZYA.Text = EditDriver.Salary.ToString();
            }
            catch { }
        }

        private void textBoxSearch_ZYA_TextChanged(object sender, EventArgs e)
        {
            CreateTableDrivers();
        }

        //-------------------------------------------------------------------------------------------------------------------------------------------
        //Перевозки

        Transportation EditTransportation;
        List<Transportation> transportation;
        string pathTransportation = Directory.GetCurrentDirectory() + "\\Transportation.csv";

        bool departureDate = false;
        bool arrivalDate = false;

        private void CreateTableTransportation()
        {
            transportation = action.DataOutputTransportation(pathTransportation);
            List<Transportation> transportationFilter = transportation;
            if (radioButtonSortingDepartureDate_ZYA.Checked)
            {
                if (departureDate)
                {
                    transportationFilter = transportationFilter.OrderBy(s => s.DepartureDate).ToList();
                }
                else
                {
                    transportationFilter = transportationFilter.OrderByDescending(s => s.DepartureDate).ToList();
                }
            }
            else
            {
                if (arrivalDate)
                {
                    transportationFilter = transportationFilter.OrderBy(s => s.ArrivalDate).ToList();
                }
                else
                {
                    transportationFilter = transportationFilter.OrderByDescending(s => s.ArrivalDate).ToList();
                }
            }
            dataGridViewTransportation_ZYA.DataSource = transportationFilter.ToArray();
        }

        private void buttonTransportationAdd_ZYA_Click(object sender, EventArgs e)
        {
            try
            {
                int id = 0;
                if (transportation.Count > 0) id = (int)transportation.Last().ID + 1;
                transportation.Add(new Transportation()
                {
                    ID = id,
                    CarsID = (comboBoxCarId_ZYA.SelectedItem as Cars).ID,
                    DriversID = (comboBoxDriverId_ZYA.SelectedItem as Drivers).ID,
                    DepartureDate = Convert.ToDateTime(dateTimePickerDepartureDate_ZYA.Text),
                    ArrivalDate = Convert.ToDateTime(dateTimePickerArrivalDate_ZYA.Text),
                    Destination = textBoxDestination_ZYA.Text,
                    Distance = Convert.ToDouble(textBoxDistance_ZYA.Text),
                    FuelConsumption = Convert.ToDouble(textBoxFuelConsumptionTransportation_ZYA.Text),
                    CargoWeight = Convert.ToDouble(textBoxCargoWeight_ZYA.Text),
                });
                action.DataEntryTransportation(transportation, pathTransportation);
                CreateTableTransportation();
            }
            catch
            {
                MessageBox.Show("Не верный ввод данных", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonTransportationEdit_ZYA_Click(object sender, EventArgs e)
        {
            try
            {
                EditTransportation.CarsID = (comboBoxCarId_ZYA.SelectedItem as Cars).ID;
                EditTransportation.DriversID = (comboBoxDriverId_ZYA.SelectedItem as Drivers).ID;
                EditTransportation.DepartureDate = Convert.ToDateTime(dateTimePickerDepartureDate_ZYA.Text);
                EditTransportation.ArrivalDate = Convert.ToDateTime(dateTimePickerArrivalDate_ZYA.Text);
                EditTransportation.Destination = textBoxDestination_ZYA.Text;
                EditTransportation.Distance = Convert.ToDouble(textBoxDistance_ZYA.Text);
                EditTransportation.FuelConsumption = Convert.ToDouble(textBoxFuelConsumptionTransportation_ZYA.Text);
                EditTransportation.CargoWeight = Convert.ToDouble(textBoxCargoWeight_ZYA.Text);
                action.DataEntryTransportation(transportation, pathTransportation);
                CreateTableTransportation();
            
                buttonTransportationDelete_ZYA.Enabled = false;
                buttonTransportationEdit_ZYA.Enabled = false;
            }
            catch
            {
                MessageBox.Show("Не верный ввод данных", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonTransportationDelete_ZYA_Click(object sender, EventArgs e)
        {
            try
            {
                transportation.Remove(EditTransportation);
                action.DataEntryTransportation(transportation, pathTransportation);
                CreateTableTransportation();
                buttonTransportationDelete_ZYA.Enabled = false;
                buttonTransportationEdit_ZYA.Enabled = false;
            }
            catch { }
        }

        private void radioButtonSortingDepartureDate_ZYA_Click(object sender, EventArgs e)
        {
            if (radioButtonSortingDepartureDate_ZYA.Checked) departureDate = !departureDate;

            if (departureDate) radioButtonSortingDepartureDate_ZYA.Image = Tyuiu.ZhukovaYA.Sprint7.V8.Properties.Resources.arrow_up;
            else radioButtonSortingDepartureDate_ZYA.Image = Tyuiu.ZhukovaYA.Sprint7.V8.Properties.Resources.arrow_down;

            radioButtonSortingArrivalDate_ZYA.Image = Tyuiu.ZhukovaYA.Sprint7.V8.Properties.Resources.bullet_green;
            CreateTableTransportation();
        }

        private void radioButtonSortingArrivalDate_ZYA_Click(object sender, EventArgs e)
        {
            if (radioButtonSortingArrivalDate_ZYA.Checked) arrivalDate = !arrivalDate;

            if (arrivalDate) radioButtonSortingArrivalDate_ZYA.Image = Tyuiu.ZhukovaYA.Sprint7.V8.Properties.Resources.arrow_up;
            else radioButtonSortingArrivalDate_ZYA.Image = Tyuiu.ZhukovaYA.Sprint7.V8.Properties.Resources.arrow_down;

            radioButtonSortingDepartureDate_ZYA.Image = Tyuiu.ZhukovaYA.Sprint7.V8.Properties.Resources.bullet_green;
            CreateTableTransportation();
        }

        private void textBoxCargoWeight_ZYA_TextChanged(object sender, EventArgs e)
        {
            try
            {
                comboBoxCarId_ZYA.SelectedIndex = -1;
                if (textBoxCargoWeight_ZYA.Text == "")
                {
                    comboBoxCarId_ZYA.Enabled = false;
                    return;
                }
                    comboBoxCarId_ZYA.Enabled = true;
                    List<Cars> carsFilter = action.CheckingTheCar(cars, Convert.ToInt32(textBoxCargoWeight_ZYA.Text));
                    comboBoxCarId_ZYA.DataSource = carsFilter;
            }
            catch
            {
                comboBoxCarId_ZYA.Enabled = false;
            }
        }

        private void textBoxDistance_ZYA_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (comboBoxCarId_ZYA.SelectedIndex > -1 && textBoxDistance_ZYA.Text != "")
                    textBoxFuelConsumptionTransportation_ZYA.Text = action.FuelConsumptionPerDistanceTraveled(comboBoxCarId_ZYA.SelectedItem as Cars, Convert.ToDouble(textBoxDistance_ZYA.Text)).ToString();
            }
            catch
            {
                textBoxFuelConsumptionTransportation_ZYA.Text = "";
            }
        }

        private void comboBoxCarId_ZYA_DropDownClosed(object sender, EventArgs e)
        {
            try
            {
                if (comboBoxCarId_ZYA.SelectedIndex > -1 && textBoxDistance_ZYA.Text != "")
                textBoxFuelConsumptionTransportation_ZYA.Text = action.FuelConsumptionPerDistanceTraveled(comboBoxCarId_ZYA.SelectedItem as Cars, Convert.ToDouble(textBoxDistance_ZYA.Text)).ToString();
            }
            catch
            {
                textBoxFuelConsumptionTransportation_ZYA.Text = "";
            }
        }

        private void dataGridViewTransportation_ZYA_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                EditTransportation = (Transportation)transportation.Where(a => a.ID == Convert.ToInt32(dataGridViewTransportation_ZYA[0, e.RowIndex].Value.ToString())).First();
                buttonTransportationDelete_ZYA.Enabled = true;
                buttonTransportationEdit_ZYA.Enabled = true;
                dateTimePickerDepartureDate_ZYA.Text = EditTransportation.DepartureDate.ToString();
                dateTimePickerArrivalDate_ZYA.Text = EditTransportation.ArrivalDate.ToString(); 
                textBoxDestination_ZYA.Text = EditTransportation.Destination.ToString(); 
                textBoxDistance_ZYA.Text = EditTransportation.Distance.ToString(); 
                textBoxFuelConsumptionTransportation_ZYA.Text = EditTransportation.FuelConsumption.ToString(); 
                textBoxCargoWeight_ZYA.Text = EditTransportation.CargoWeight.ToString(); 
                comboBoxCarId_ZYA.SelectedIndex = comboBoxCarId_ZYA.FindStringExact(cars.Where(s => s.ID == EditTransportation.CarsID).First().LicensePlate.ToString());
                comboBoxDriverId_ZYA.SelectedIndex = comboBoxDriverId_ZYA.FindStringExact(drivers.Where(s => s.ID == EditTransportation.DriversID).First().ServiceNumber.ToString());
            }
            catch { }
        }

        private void buttonTransportationChart_ZYA_Click(object sender, EventArgs e)
        {
            try
            {
                chartTransportation_ZYA.Titles.Clear();
                chartTransportation_ZYA.Series.Clear();
                chartTransportation_ZYA.Titles.Add("Статистика поездок");
                string[] driversName = drivers.Select(s => s.FIO).ToArray();
                int[] countTransportation = new int[driversName.Length];
                foreach (var item in transportation)
                {
                    for (int i = 0; i < driversName.Length; i++)
                    {
                        if (drivers.Where(s => s.ID == item.DriversID).Select(s => s.FIO).First().ToString() == driversName[i].ToString()) countTransportation[i]++;
                    }
                }

                for (int i = 0; i < driversName.Length; i++)
                {
                    Series series = chartTransportation_ZYA.Series.Add(driversName[i]);
                    series.Points.Add(countTransportation[i]);
                }
            }
            catch
            {

            }
        }

        private void оПриложенииToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutTheProgram aboutTheProgram = new AboutTheProgram();
            aboutTheProgram.Show();
        }

        private void открытьПапкуToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Process.Start(Application.StartupPath);
        }

        private void выходToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
