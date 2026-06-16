using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace Tyuiu.SklyarovAM.Sprint7.V1
{
    public partial class MainForm_SAM : Form
    {
        List<Car> cars_SAM = new List<Car>();
        List<Car> filteredCars_SAM = new List<Car>();
        string carsFile_SAM = "cars.csv";
        DataGridView dataGridView_SAM;
        TextBox txtPlate_SAM, txtBrand_SAM, txtColor_SAM, txtOwnerName_SAM, txtOwnerPhone_SAM, txtOwnerLicense_SAM, txtSearch_SAM;
        NumericUpDown numPower_SAM;
        Button btnAdd_SAM, btnEdit_SAM, btnDelete_SAM, btnSave_SAM, btnSort_SAM, btnShowAll_SAM;
        Label lblTotal_SAM, lblAvg_SAM, lblMin_SAM, lblMax_SAM;
        Panel chartPanel_SAM;
        StatusStrip statusStrip_SAM;
        ToolTip toolTip_SAM;

        public MainForm_SAM()
        {
            this.Text = "Скляров Антон Михайлович ИИПб-25-1";
            this.Size = new Size(1200, 750);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = Color.LightGray;
            toolTip_SAM = new ToolTip();
            CreateMenu();
            CreateToolbar();
            CreateDataGrid();
            CreateInputPanel();
            CreateSearchPanel();
            CreateStatsPanel();
            CreateChartPanel();
            CreateStatusBar();
            LoadData();
            UpdateGrid();
            UpdateStats();
            DrawChart();
        }

        void CreateMenu()
        {
            MenuStrip menu = new MenuStrip();
            ToolStripMenuItem fileMenu = new ToolStripMenuItem("Файл");
            ToolStripMenuItem exitItem = new ToolStripMenuItem("Выход", null, (s, e) => Application.Exit());
            fileMenu.DropDownItems.Add(exitItem);
            ToolStripMenuItem dataMenu = new ToolStripMenuItem("Данные");
            ToolStripMenuItem saveItem = new ToolStripMenuItem("Сохранить", null, (s, e) => SaveData());
            ToolStripMenuItem loadItem = new ToolStripMenuItem("Загрузить", null, (s, e) => LoadData());
            dataMenu.DropDownItems.Add(saveItem);
            dataMenu.DropDownItems.Add(loadItem);
            ToolStripMenuItem helpMenu = new ToolStripMenuItem("Справка");
            ToolStripMenuItem aboutItem = new ToolStripMenuItem("О программе", null, ShowAbout);
            ToolStripMenuItem guideItem = new ToolStripMenuItem("Руководство", null, ShowGuide);
            helpMenu.DropDownItems.Add(aboutItem);
            helpMenu.DropDownItems.Add(guideItem);
            menu.Items.Add(fileMenu);
            menu.Items.Add(dataMenu);
            menu.Items.Add(helpMenu);
            this.Controls.Add(menu);
        }

        void CreateToolbar()
        {
            ToolStrip toolBar = new ToolStrip();
            ToolStripButton btnAddTool = new ToolStripButton("Добавить");
            btnAddTool.ToolTipText = "Добавить новую машину";
            btnAddTool.Click += (s, e) => AddCar();
            ToolStripButton btnEditTool = new ToolStripButton("Изменить");
            btnEditTool.ToolTipText = "Изменить выбранную машину";
            btnEditTool.Click += (s, e) => EditCar();
            ToolStripButton btnDeleteTool = new ToolStripButton("Удалить");
            btnDeleteTool.ToolTipText = "Удалить выбранную машину";
            btnDeleteTool.Click += (s, e) => DeleteCar();
            ToolStripButton btnSaveTool = new ToolStripButton("Сохранить");
            btnSaveTool.ToolTipText = "Сохранить всё в файл";
            btnSaveTool.Click += (s, e) => SaveData();
            toolBar.Items.Add(btnAddTool);
            toolBar.Items.Add(btnEditTool);
            toolBar.Items.Add(btnDeleteTool);
            toolBar.Items.Add(btnSaveTool);
            this.Controls.Add(toolBar);
        }

        void CreateDataGrid()
        {
            dataGridView_SAM = new DataGridView();
            dataGridView_SAM.Location = new Point(10, 100);
            dataGridView_SAM.Size = new Size(750, 350);
            dataGridView_SAM.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView_SAM.MultiSelect = false;
            dataGridView_SAM.BackgroundColor = Color.White;
            dataGridView_SAM.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView_SAM.AllowUserToAddRows = false;
            this.Controls.Add(dataGridView_SAM);
        }

        void CreateInputPanel()
        {
            GroupBox group = new GroupBox();
            group.Text = "Ввод данных об автомобиле и владельце";
            group.Location = new Point(770, 100);
            group.Size = new Size(400, 380);
            group.Font = new Font("Arial", 9, FontStyle.Bold);
            int y = 25;
            int labelWidth = 120;
            int controlWidth = 230;
            Label lblCarTitle = new Label() { Text = "ДАННЫЕ ОБ АВТОМОБИЛЕ:", Location = new Point(10, y), Size = new Size(380, 20), Font = new Font("Arial", 9, FontStyle.Bold), ForeColor = Color.DarkBlue };
            y += 25;
            Label lblPlate = new Label() { Text = "Госномер:", Location = new Point(10, y), Size = new Size(labelWidth, 25) };
            txtPlate_SAM = new TextBox() { Location = new Point(labelWidth + 10, y), Size = new Size(controlWidth, 25) };
            toolTip_SAM.SetToolTip(txtPlate_SAM, "Введите государственный номер автомобиля");
            y += 35;
            Label lblBrand = new Label() { Text = "Марка:", Location = new Point(10, y), Size = new Size(labelWidth, 25) };
            txtBrand_SAM = new TextBox() { Location = new Point(labelWidth + 10, y), Size = new Size(controlWidth, 25) };
            toolTip_SAM.SetToolTip(txtBrand_SAM, "Введите марку автомобиля");
            y += 35;
            Label lblPower = new Label() { Text = "Мощность (л.с.):", Location = new Point(10, y), Size = new Size(labelWidth, 25) };
            numPower_SAM = new NumericUpDown() { Location = new Point(labelWidth + 10, y), Size = new Size(controlWidth, 25), Minimum = 1, Maximum = 2000, Value = 100 };
            toolTip_SAM.SetToolTip(numPower_SAM, "Введите мощность двигателя");
            y += 35;
            Label lblColor = new Label() { Text = "Цвет:", Location = new Point(10, y), Size = new Size(labelWidth, 25) };
            txtColor_SAM = new TextBox() { Location = new Point(labelWidth + 10, y), Size = new Size(controlWidth, 25) };
            txtColor_SAM.Text = "";
            toolTip_SAM.SetToolTip(txtColor_SAM, "Введите любой цвет");
            y += 45;
            Label lblOwnerTitle = new Label() { Text = "ДАННЫЕ О ВЛАДЕЛЬЦЕ:", Location = new Point(10, y), Size = new Size(380, 20), Font = new Font("Arial", 9, FontStyle.Bold), ForeColor = Color.DarkBlue };
            y += 25;
            Label lblOwnerName = new Label() { Text = "ФИО владельца:", Location = new Point(10, y), Size = new Size(labelWidth, 25) };
            txtOwnerName_SAM = new TextBox() { Location = new Point(labelWidth + 10, y), Size = new Size(controlWidth, 25) };
            txtOwnerName_SAM.Text = "";
            toolTip_SAM.SetToolTip(txtOwnerName_SAM, "Введите ФИО владельца");
            y += 35;
            Label lblOwnerPhone = new Label() { Text = "Телефон:", Location = new Point(10, y), Size = new Size(labelWidth, 25) };
            txtOwnerPhone_SAM = new TextBox() { Location = new Point(labelWidth + 10, y), Size = new Size(controlWidth, 25) };
            txtOwnerPhone_SAM.Text = "";
            toolTip_SAM.SetToolTip(txtOwnerPhone_SAM, "Введите номер телефона (необязательно)");
            y += 35;
            Label lblOwnerLicense = new Label() { Text = "Номер ВУ:", Location = new Point(10, y), Size = new Size(labelWidth, 25) };
            txtOwnerLicense_SAM = new TextBox() { Location = new Point(labelWidth + 10, y), Size = new Size(controlWidth, 25) };
            txtOwnerLicense_SAM.Text = "";
            toolTip_SAM.SetToolTip(txtOwnerLicense_SAM, "Введите номер ВУ (необязательно)");
            y += 45;
            btnAdd_SAM = new Button() { Text = "Добавить", Location = new Point(50, y), Size = new Size(90, 35), BackColor = Color.LightGreen };
            btnEdit_SAM = new Button() { Text = "Изменить", Location = new Point(155, y), Size = new Size(90, 35), BackColor = Color.LightBlue };
            btnDelete_SAM = new Button() { Text = "Удалить", Location = new Point(260, y), Size = new Size(90, 35), BackColor = Color.LightCoral };
            toolTip_SAM.SetToolTip(btnAdd_SAM, "Добавить новый автомобиль");
            toolTip_SAM.SetToolTip(btnEdit_SAM, "Изменить выбранный автомобиль");
            toolTip_SAM.SetToolTip(btnDelete_SAM, "Удалить выбранный автомобиль");
            btnAdd_SAM.Click += (s, e) => AddCar();
            btnEdit_SAM.Click += (s, e) => EditCar();
            btnDelete_SAM.Click += (s, e) => DeleteCar();
            group.Controls.Add(lblCarTitle);
            group.Controls.Add(lblPlate);
            group.Controls.Add(txtPlate_SAM);
            group.Controls.Add(lblBrand);
            group.Controls.Add(txtBrand_SAM);
            group.Controls.Add(lblPower);
            group.Controls.Add(numPower_SAM);
            group.Controls.Add(lblColor);
            group.Controls.Add(txtColor_SAM);
            group.Controls.Add(lblOwnerTitle);
            group.Controls.Add(lblOwnerName);
            group.Controls.Add(txtOwnerName_SAM);
            group.Controls.Add(lblOwnerPhone);
            group.Controls.Add(txtOwnerPhone_SAM);
            group.Controls.Add(lblOwnerLicense);
            group.Controls.Add(txtOwnerLicense_SAM);
            group.Controls.Add(btnAdd_SAM);
            group.Controls.Add(btnEdit_SAM);
            group.Controls.Add(btnDelete_SAM);
            this.Controls.Add(group);
        }

        void CreateSearchPanel()
        {
            GroupBox group = new GroupBox();
            group.Text = "Поиск и сортировка";
            group.Location = new Point(10, 460);
            group.Size = new Size(750, 80);
            Label lblSearch = new Label() { Text = "Поиск:", Location = new Point(10, 25), Size = new Size(50, 25) };
            txtSearch_SAM = new TextBox() { Location = new Point(60, 25), Size = new Size(200, 25) };
            txtSearch_SAM.TextChanged += (s, e) => SearchCars();
            toolTip_SAM.SetToolTip(txtSearch_SAM, "Введите текст для поиска");
            Button btnSearch = new Button() { Text = "Найти", Location = new Point(270, 23), Size = new Size(80, 30) };
            btnSearch.Click += (s, e) => SearchCars();
            toolTip_SAM.SetToolTip(btnSearch, "Найти автомобили");
            btnSort_SAM = new Button() { Text = "Сортировка по мощности", Location = new Point(370, 23), Size = new Size(160, 30) };
            btnSort_SAM.Click += (s, e) => SortCars();
            toolTip_SAM.SetToolTip(btnSort_SAM, "Отсортировать по мощности");
            btnShowAll_SAM = new Button() { Text = "Показать все", Location = new Point(550, 23), Size = new Size(100, 30) };
            btnShowAll_SAM.Click += (s, e) => ShowAllCars();
            toolTip_SAM.SetToolTip(btnShowAll_SAM, "Показать все автомобили");
            group.Controls.Add(lblSearch);
            group.Controls.Add(txtSearch_SAM);
            group.Controls.Add(btnSearch);
            group.Controls.Add(btnSort_SAM);
            group.Controls.Add(btnShowAll_SAM);
            this.Controls.Add(group);
        }

        void CreateStatsPanel()
        {
            GroupBox group = new GroupBox();
            group.Text = "Статистика";
            group.Location = new Point(770, 460);
            group.Size = new Size(400, 100);
            group.Font = new Font("Arial", 9, FontStyle.Bold);
            lblTotal_SAM = new Label() { Location = new Point(10, 25), Size = new Size(180, 25), Font = new Font("Arial", 9, FontStyle.Regular) };
            lblAvg_SAM = new Label() { Location = new Point(10, 50), Size = new Size(180, 25), Font = new Font("Arial", 9, FontStyle.Regular) };
            lblMin_SAM = new Label() { Location = new Point(200, 25), Size = new Size(180, 25), Font = new Font("Arial", 9, FontStyle.Regular) };
            lblMax_SAM = new Label() { Location = new Point(200, 50), Size = new Size(180, 25), Font = new Font("Arial", 9, FontStyle.Regular) };
            group.Controls.Add(lblTotal_SAM);
            group.Controls.Add(lblAvg_SAM);
            group.Controls.Add(lblMin_SAM);
            group.Controls.Add(lblMax_SAM);
            this.Controls.Add(group);
        }

        void CreateChartPanel()
        {
            GroupBox group = new GroupBox();
            group.Text = "График распределения мощности автомобилей";
            group.Location = new Point(10, 550);
            group.Size = new Size(1160, 150);
            group.Font = new Font("Arial", 9, FontStyle.Bold);
            chartPanel_SAM = new Panel();
            chartPanel_SAM.Location = new Point(10, 20);
            chartPanel_SAM.Size = new Size(1140, 120);
            chartPanel_SAM.BackColor = Color.White;
            group.Controls.Add(chartPanel_SAM);
            this.Controls.Add(group);
        }

        void CreateStatusBar()
        {
            statusStrip_SAM = new StatusStrip();
            ToolStripStatusLabel statusLabel = new ToolStripStatusLabel("Программа запущена. Добавьте первый автомобиль.");
            statusStrip_SAM.Items.Add(statusLabel);
            this.Controls.Add(statusStrip_SAM);
        }

        public class Car
        {
            public string Plate { get; set; }
            public string Brand { get; set; }
            public int Power { get; set; }
            public string Color { get; set; }
            public string OwnerName { get; set; }
            public string OwnerPhone { get; set; }
            public string OwnerLicense { get; set; }
        }

        void LoadData()
        {
            cars_SAM.Clear();
            if (File.Exists(carsFile_SAM))
            {
                string[] lines = File.ReadAllLines(carsFile_SAM);
                foreach (string line in lines)
                {
                    if (!string.IsNullOrWhiteSpace(line))
                    {
                        string[] parts = line.Split(';');
                        if (parts.Length >= 7)
                        {
                            Car c = new Car();
                            c.Plate = parts[0];
                            c.Brand = parts[1];
                            c.Power = int.Parse(parts[2]);
                            c.Color = parts[3];
                            c.OwnerName = parts[4];
                            c.OwnerPhone = parts[5];
                            c.OwnerLicense = parts[6];
                            cars_SAM.Add(c);
                        }
                    }
                }
                statusStrip_SAM.Items[0].Text = $"Загружено {cars_SAM.Count} автомобилей";
            }
            else
            {
                statusStrip_SAM.Items[0].Text = "Файл данных не найден. Начните добавлять автомобили.";
            }
            filteredCars_SAM = new List<Car>(cars_SAM);
        }

        void SaveData()
        {
            List<string> lines = new List<string>();
            foreach (Car c in cars_SAM)
            {
                lines.Add($"{c.Plate};{c.Brand};{c.Power};{c.Color};{c.OwnerName};{c.OwnerPhone};{c.OwnerLicense}");
            }
            File.WriteAllLines(carsFile_SAM, lines);
            statusStrip_SAM.Items[0].Text = $"Сохранено {cars_SAM.Count} автомобилей";
            MessageBox.Show("Данные сохранены!", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        void UpdateGrid()
        {
            var display = new List<object>();
            foreach (Car c in filteredCars_SAM)
            {
                display.Add(new
                {
                    Госномер = c.Plate,
                    Марка = c.Brand,
                    Мощность_лс = c.Power,
                    Цвет = c.Color,
                    Владелец = c.OwnerName,
                    Телефон = c.OwnerPhone,
                    Номер_ВУ = c.OwnerLicense
                });
            }
            dataGridView_SAM.DataSource = null;
            dataGridView_SAM.DataSource = display;
        }

        void UpdateStats()
        {
            if (filteredCars_SAM.Count == 0)
            {
                lblTotal_SAM.Text = "Всего: 0";
                lblAvg_SAM.Text = "Средняя: 0";
                lblMin_SAM.Text = "Мин: 0";
                lblMax_SAM.Text = "Макс: 0";
                return;
            }
            int total = filteredCars_SAM.Count;
            int sumPower = 0;
            int minPower = filteredCars_SAM[0].Power;
            int maxPower = filteredCars_SAM[0].Power;
            foreach (Car c in filteredCars_SAM)
            {
                sumPower += c.Power;
                if (c.Power < minPower) minPower = c.Power;
                if (c.Power > maxPower) maxPower = c.Power;
            }
            double avgPower = (double)sumPower / total;
            lblTotal_SAM.Text = $"Всего машин: {total}";
            lblAvg_SAM.Text = $"Средняя мощность: {avgPower:F1} л.с.";
            lblMin_SAM.Text = $"Мин. мощность: {minPower} л.с.";
            lblMax_SAM.Text = $"Макс. мощность: {maxPower} л.с.";
        }

        void DrawChart()
        {
            chartPanel_SAM.Controls.Clear();
            if (filteredCars_SAM.Count == 0)
            {
                Label noData = new Label() { Text = "Нет данных", Location = new Point(450, 50), Size = new Size(250, 25), Font = new Font("Arial", 10, FontStyle.Bold), ForeColor = Color.Gray };
                chartPanel_SAM.Controls.Add(noData);
                return;
            }
            var groups = new Dictionary<string, int>();
            foreach (Car c in filteredCars_SAM)
            {
                int range = (c.Power / 50) * 50;
                string key = $"{range}-{range + 50}";
                if (groups.ContainsKey(key))
                    groups[key]++;
                else
                    groups[key] = 1;
            }
            int maxCount = 0;
            foreach (int count in groups.Values)
            {
                if (count > maxCount) maxCount = count;
            }
            if (maxCount == 0) maxCount = 1;
            int barWidth = chartPanel_SAM.Width / groups.Count - 10;
            if (barWidth < 5) barWidth = 5;
            if (barWidth > 80) barWidth = 80;
            int x = 5;
            foreach (var pair in groups)
            {
                int barHeight = (pair.Value * (chartPanel_SAM.Height - 50)) / maxCount;
                if (barHeight < 5 && pair.Value > 0) barHeight = 5;
                Panel bar = new Panel();
                bar.BackColor = Color.SteelBlue;
                bar.Size = new Size(barWidth, barHeight);
                bar.Location = new Point(x, chartPanel_SAM.Height - barHeight - 25);
                Label label = new Label();
                label.Text = pair.Key;
                label.Font = new Font("Arial", 7);
                label.Size = new Size(barWidth + 5, 15);
                label.Location = new Point(x - 2, chartPanel_SAM.Height - 22);
                Label countLabel = new Label();
                countLabel.Text = pair.Value.ToString();
                countLabel.Font = new Font("Arial", 7);
                countLabel.Size = new Size(barWidth + 5, 12);
                countLabel.Location = new Point(x - 2, chartPanel_SAM.Height - barHeight - 38);
                chartPanel_SAM.Controls.Add(bar);
                chartPanel_SAM.Controls.Add(label);
                chartPanel_SAM.Controls.Add(countLabel);
                x += barWidth + 8;
            }
        }

        void AddCar()
        {
            if (string.IsNullOrWhiteSpace(txtPlate_SAM.Text))
            {
                MessageBox.Show("Введите госномер!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (string.IsNullOrWhiteSpace(txtBrand_SAM.Text))
            {
                MessageBox.Show("Введите марку!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (string.IsNullOrWhiteSpace(txtColor_SAM.Text))
            {
                MessageBox.Show("Введите цвет!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (string.IsNullOrWhiteSpace(txtOwnerName_SAM.Text))
            {
                MessageBox.Show("Введите ФИО владельца!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            foreach (Car existing in cars_SAM)
            {
                if (existing.Plate.ToLower() == txtPlate_SAM.Text.ToLower())
                {
                    MessageBox.Show("Автомобиль с таким госномером уже существует!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }
            Car newCar = new Car();
            newCar.Plate = txtPlate_SAM.Text;
            newCar.Brand = txtBrand_SAM.Text;
            newCar.Power = (int)numPower_SAM.Value;
            newCar.Color = txtColor_SAM.Text;
            newCar.OwnerName = txtOwnerName_SAM.Text;
            newCar.OwnerPhone = txtOwnerPhone_SAM.Text;
            newCar.OwnerLicense = txtOwnerLicense_SAM.Text;
            cars_SAM.Add(newCar);
            filteredCars_SAM.Add(newCar);
            SaveData();
            UpdateGrid();
            UpdateStats();
            DrawChart();
            ClearInput();
            statusStrip_SAM.Items[0].Text = $"Добавлен автомобиль {newCar.Plate}";
        }

        void EditCar()
        {
            if (dataGridView_SAM.SelectedRows.Count == 0)
            {
                MessageBox.Show("Выберите автомобиль!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (string.IsNullOrWhiteSpace(txtBrand_SAM.Text))
            {
                MessageBox.Show("Введите марку!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (string.IsNullOrWhiteSpace(txtColor_SAM.Text))
            {
                MessageBox.Show("Введите цвет!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (string.IsNullOrWhiteSpace(txtOwnerName_SAM.Text))
            {
                MessageBox.Show("Введите ФИО владельца!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            int index = dataGridView_SAM.SelectedRows[0].Index;
            if (index >= filteredCars_SAM.Count) return;
            Car car = filteredCars_SAM[index];
            Car original = null;
            foreach (Car c in cars_SAM)
            {
                if (c.Plate == car.Plate)
                {
                    original = c;
                    break;
                }
            }
            if (original != null)
            {
                original.Brand = txtBrand_SAM.Text;
                original.Power = (int)numPower_SAM.Value;
                original.Color = txtColor_SAM.Text;
                original.OwnerName = txtOwnerName_SAM.Text;
                original.OwnerPhone = txtOwnerPhone_SAM.Text;
                original.OwnerLicense = txtOwnerLicense_SAM.Text;
                SaveData();
                filteredCars_SAM = new List<Car>(cars_SAM);
                UpdateGrid();
                UpdateStats();
                DrawChart();
                ClearInput();
                statusStrip_SAM.Items[0].Text = $"Автомобиль {original.Plate} изменён";
            }
        }

        void DeleteCar()
        {
            if (dataGridView_SAM.SelectedRows.Count == 0)
            {
                MessageBox.Show("Выберите автомобиль!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            DialogResult res = MessageBox.Show("Удалить выбранный автомобиль?", "Подтверждение", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (res == DialogResult.Yes)
            {
                int index = dataGridView_SAM.SelectedRows[0].Index;
                if (index >= filteredCars_SAM.Count) return;
                Car car = filteredCars_SAM[index];
                Car toRemove = null;
                foreach (Car c in cars_SAM)
                {
                    if (c.Plate == car.Plate)
                    {
                        toRemove = c;
                        break;
                    }
                }
                if (toRemove != null)
                    cars_SAM.Remove(toRemove);
                filteredCars_SAM = new List<Car>(cars_SAM);
                SaveData();
                UpdateGrid();
                UpdateStats();
                DrawChart();
                statusStrip_SAM.Items[0].Text = $"Автомобиль {car.Plate} удалён";
            }
        }
        void SearchCars()
        {
            string search = txtSearch_SAM.Text.ToLower();
            if (string.IsNullOrWhiteSpace(search))
            {
                filteredCars_SAM = new List<Car>(cars_SAM);
            }
            else
            {
                filteredCars_SAM = new List<Car>();
                foreach (Car c in cars_SAM)
                {
                    if (c.Plate.ToLower().Contains(search) ||
                        c.Brand.ToLower().Contains(search) ||
                        c.Color.ToLower().Contains(search) ||
                        c.OwnerName.ToLower().Contains(search) ||
                        c.OwnerPhone.Contains(search) ||
                        c.OwnerLicense.ToLower().Contains(search))
                    {
                        filteredCars_SAM.Add(c);
                    }
                }
            }
            UpdateGrid();
            UpdateStats();
            DrawChart();
            statusStrip_SAM.Items[0].Text = $"Найдено {filteredCars_SAM.Count} из {cars_SAM.Count}";
        }
        void SortCars()
        {
            if (filteredCars_SAM.Count == 0)
            {
                MessageBox.Show("Нет данных для сортировки!", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            List<Car> sorted = new List<Car>(filteredCars_SAM);
            for (int i = 0; i < sorted.Count - 1; i++)
            {
                for (int j = 0; j < sorted.Count - 1 - i; j++)
                {
                    if (sorted[j].Power > sorted[j + 1].Power)
                    {
                        Car temp = sorted[j];
                        sorted[j] = sorted[j + 1];
                        sorted[j + 1] = temp;
                    }
                }
            }
            filteredCars_SAM = sorted;
            UpdateGrid();
            statusStrip_SAM.Items[0].Text = "Отсортировано по мощности";
        }
        void ShowAllCars()
        {
            filteredCars_SAM = new List<Car>(cars_SAM);
            txtSearch_SAM.Text = "";
            UpdateGrid();
            UpdateStats();
            DrawChart();
            statusStrip_SAM.Items[0].Text = $"Показаны все автомобили ({cars_SAM.Count})";
        }
        void ClearInput()
        {
            txtPlate_SAM.Text = "";
            txtBrand_SAM.Text = "";
            numPower_SAM.Value = 100;
            txtColor_SAM.Text = "";
            txtOwnerName_SAM.Text = "";
            txtOwnerPhone_SAM.Text = "";
            txtOwnerLicense_SAM.Text = "";
        }

        void ShowAbout(object sender, EventArgs e)
        {
            MessageBox.Show("Авторемонтная мастерская\n Выполнил: Скляров А.М.\n Группа ИИПб-25-1", "О программе", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        void ShowGuide(object sender, EventArgs e)
        {
            MessageBox.Show("1. Добавление: заполните поля и нажмите 'Добавить'\n2. Редактирование: выберите строку, измените поля, нажмите 'Изменить'\n3. Удаление: выберите строку, нажмите 'Удалить'\n4. Поиск: введите текст в поле поиска\n5. Сортировка: нажмите 'Сортировка по мощности'\n6. Сохранение: нажмите 'Сохранить'", "Руководство", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
