using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace _2048WinFormsApp
{
    public partial class MainForm : Form
    {
        private const int lableSize = 65;
        private const int padding = 8;
        private const int startX = 10;
        private const int startY = 70;

        private Label[,] labelsMap;
        private int mapSize = 4;       
        private int score = 0;
        private int bestScore = 0;
        private User user;
        private string name = "";
        public MainForm()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            var welcomForm = new WelcomForm();  
            welcomForm.ShowDialog();
            user = new User(welcomForm.userNameTextBox.Text, 0);
            name = welcomForm.userNameTextBox.Text;

            CalculateMapSize(welcomForm.radioButtons);

            InitMap();
            GenerateNumber();
            ShowScore();
            CalculateBestScore();
        }

        private void CalculateMapSize(List<RadioButton> radioButtons)
        {
            foreach(var item in radioButtons)
            {
                if (item.Checked)
                {
                    mapSize = Convert.ToInt32(item.Text[0].ToString());
                    break;
                }
            }
        }

        private void CalculateBestScore()
        {
            var users = UserManager.GetAll();
            if (users.Count == 0)
            {
                return;
            }
            bestScore = users[0].Score;
            foreach (var user in users)
            {
                if (user.Score > bestScore)
                {
                    bestScore = user.Score;
                }
            }
            ShowBestScore();
        }

        private void ShowScore()
        {
            scoreLabel.Text = score.ToString(); 
        }

        private void ShowBestScore()
        {
            if (score > bestScore)
            {
                bestScore = score;
            }
            bestScoreLabel.Text = bestScore.ToString();
        }

        private void InitMap()
        {
            ClientSize = new Size(startX + ((lableSize + padding) * mapSize), startY + ((lableSize + padding) * mapSize));

            labelsMap = new Label[mapSize, mapSize];
            for (int i = 0; i < mapSize; i++)
            {
                for (int j = 0; j < mapSize; j++)
                {
                    var newLabel = CreateLabel(i, j);
                    Controls.Add(newLabel);
                    labelsMap[i, j] = newLabel;
                }
            }
        }

        private void GenerateNumber()
        {
            Random random = new Random();
            for(int i = 0; i < mapSize*mapSize; i++)
            {
                var randomNumberLabel = random.Next(mapSize * mapSize);
                var indexRow = randomNumberLabel / mapSize;
                var indexColumn = randomNumberLabel % mapSize;

                if (labelsMap[indexRow, indexColumn].Text == string.Empty)
                {
                    var rundomNumber = random.Next(1, 5);
                    if (rundomNumber == 4)
                    {
                        labelsMap[indexRow, indexColumn].Text = "4";
                    }
                    else
                    {
                        labelsMap[indexRow, indexColumn].Text = "2";
                    }                  
                    break;
                }
            }
        }

        private Label CreateLabel(int indexRow, int indexColumn)
        {
            var label = new Label();
            
            label.BackColor = SystemColors.ActiveBorder;
            label.Font = new Font("Impact", 13.8F, FontStyle.Regular, GraphicsUnit.Point);          
            label.Size = new Size(lableSize, lableSize);
            label.TabIndex = 0;
            label.TextAlign = ContentAlignment.MiddleCenter;
            int x = startX + indexColumn * (lableSize + padding);
            int y = startY + indexRow * (lableSize + padding);
            label.Location = new Point(x, y);


            label.TextChanged += Label_TextChanged;
            return label;
        }

        private void Label_TextChanged(object? sender, EventArgs e)
        {
           var label = (Label)sender;   
            switch (label.Text)
            {
                case "": label.BackColor = SystemColors.ActiveBorder; break;
                case "2": label.BackColor = Color.FromArgb(238, 228,218); break;
                case "4": label.BackColor = Color.FromArgb(238, 224, 220); break;
                case "8": label.BackColor = Color.FromArgb(238, 177, 121); break;
                case "16": label.BackColor = Color.FromArgb(238, 149, 99); break;
                case "32": label.BackColor = Color.FromArgb(250, 120, 80); break;
                case "64": label.BackColor = Color.FromArgb(250, 105, 63); break;
                case "128": label.BackColor = Color.FromArgb(250, 89, 43); break;
                case "256": label.BackColor = Color.FromArgb(250, 69, 17); break;
                case "512": label.BackColor = Color.FromArgb(215, 53, 6); break;
                case "1024": label.BackColor = Color.FromArgb(250, 215, 80); break;
                case "2048": label.BackColor = Color.FromArgb(240, 185, 1); break;
            }         
        }

        private void MainForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Right && e.KeyCode != Keys.Left && e.KeyCode != Keys.Up && e.KeyCode != Keys.Down)
            {
                return;
            }

            if (e.KeyCode == Keys.Right)
            {
                MoveRight();
            }
            if (e.KeyCode == Keys.Left)
            {
                MoveLeft();
            }
            if (e.KeyCode == Keys.Up)
            {
                MoveUp();
            }
            if (e.KeyCode == Keys.Down)
            {
                MoveDown();
            }
            GenerateNumber();
            ShowScore();
            ShowBestScore();

            if (Win())
            {
                UserManager.Add(new User(Name, score) { Name = name, Score = score });
                MessageBox.Show("Ура! Вы победили!");
                return;
            }
            if (EndGame())
            {
                UserManager.Add(new User(Name, score) { Name = name, Score = score });
                MessageBox.Show("Вы проиграли :(");
                return;
            }
        }

        private bool EndGame()
        {
            for (int i = 0; i < mapSize; i++)
            {
                for (int j = 0; j < mapSize; j++)
                {
                    if (labelsMap[i, j].Text == "")
                    {
                        return false;
                    }
                }
            }
            for (int i = 0; i < mapSize - 1; i++)
            {
                for (int j = 0; j < mapSize - 1; j++)
                {
                    if (labelsMap[i, j].Text == labelsMap[i, j + 1].Text || labelsMap[i, j].Text == labelsMap[i + 1, j].Text)
                    {
                        return false;
                    }
                }
            }
            return true;    
        }
        private bool Win()
        {
            for (int i = 0; i < mapSize; i++)
            {
                for (int j = 0; j < mapSize; j++)
                {
                    if (labelsMap[i, j].Text == "2048")
                    {
                        return true;
                    }
                }
            }
            return false;
        }
        private void MoveDown()
        {
            for (int j = 0; j < mapSize; j++)
            {
                for (int i = mapSize - 1; i >= 0; i--)
                {
                    if (labelsMap[i, j].Text != string.Empty)
                    {
                        for (int k = i - 1; k >= 0; k--)
                        {
                            if (labelsMap[k, j].Text != string.Empty)
                            {
                                if (labelsMap[i, j].Text == labelsMap[k, j].Text)
                                {
                                    var number = int.Parse(labelsMap[i, j].Text);
                                    score += number * 2;
                                    labelsMap[i, j].Text = (number * 2).ToString();
                                    labelsMap[k, j].Text = string.Empty;

                                }
                                break;
                            }
                        }
                    }
                }
            }
            for (int j = 0; j < mapSize; j++)
            {
                for (int i = mapSize - 1; i >= 0; i--)
                {
                    if (labelsMap[i, j].Text == string.Empty)
                    {
                        for (int k = i - 1; k >= 0; k--)
                        {
                            if (labelsMap[k, j].Text != string.Empty)
                            {
                                labelsMap[i, j].Text = labelsMap[k, j].Text;
                                labelsMap[k, j].Text = string.Empty;
                                break;
                            }
                        }
                    }
                }
            }
        }

        private void MoveUp()
        {
            for (int j = 0; j < mapSize; j++)
            {
                for (int i = 0; i < mapSize; i++)
                {
                    if (labelsMap[i, j].Text != string.Empty)
                    {
                        for (int k = i + 1; k < mapSize; k++)
                        {
                            if (labelsMap[k, j].Text != string.Empty)
                            {
                                if (labelsMap[i, j].Text == labelsMap[k, j].Text)
                                {
                                    var number = int.Parse(labelsMap[i, j].Text);
                                    score += number * 2;
                                    labelsMap[i, j].Text = (number * 2).ToString();
                                    labelsMap[k, j].Text = string.Empty;

                                }
                                break;
                            }
                        }
                    }
                }
            }
            for (int j = 0; j < mapSize; j++)
            {
                for (int i = 0; i < mapSize; i++)
                {
                    if (labelsMap[i, j].Text == string.Empty)
                    {
                        for (int k = i + 1; k < mapSize; k++)
                        {
                            if (labelsMap[k, j].Text != string.Empty)
                            {
                                labelsMap[i, j].Text = labelsMap[k, j].Text;
                                labelsMap[k, j].Text = string.Empty;
                                break;
                            }
                        }
                    }
                }
            }
        }

        private void MoveLeft()
        {
            for (int i = 0; i < mapSize; i++)
            {
                for (int j = 0; j < mapSize; j++)
                {
                    if (labelsMap[i, j].Text != string.Empty)
                    {
                        for (int k = j + 1; k < mapSize; k++)
                        {
                            if (labelsMap[i, k].Text != string.Empty)
                            {
                                if (labelsMap[i, j].Text == labelsMap[i, k].Text)
                                {
                                    var number = int.Parse(labelsMap[i, j].Text);
                                    score += number * 2;
                                    labelsMap[i, j].Text = (number * 2).ToString();
                                    labelsMap[i, k].Text = string.Empty;

                                }
                                break;
                            }
                        }
                    }
                }
            }
            for (int i = 0; i < mapSize; i++)
            {
                for (int j = 0; j < mapSize; j++)
                {
                    if (labelsMap[i, j].Text == string.Empty)
                    {
                        for (int k = j + 1; k < mapSize; k++)
                        {
                            if (labelsMap[i, k].Text != string.Empty)
                            {
                                labelsMap[i, j].Text = labelsMap[i, k].Text;
                                labelsMap[i, k].Text = string.Empty;
                                break;
                            }
                        }
                    }
                }
            }
        }

        private void MoveRight()
        {
            for (int i = 0; i < mapSize; i++)
            {
                for (int j = mapSize - 1; j >= 0; j--)
                {
                    if (labelsMap[i, j].Text != string.Empty)
                    {
                        for (int k = j - 1; k >= 0; k--)
                        {
                            if (labelsMap[i, k].Text != string.Empty)
                            {
                                if (labelsMap[i, j].Text == labelsMap[i, k].Text)
                                {
                                    var number = int.Parse(labelsMap[i, j].Text);
                                    score += number * 2;
                                    labelsMap[i, j].Text = (number * 2).ToString();
                                    labelsMap[i, k].Text = string.Empty;

                                }
                                break;
                            }
                        }
                    }
                }
            }
            for (int i = 0; i < mapSize; i++)
            {
                for (int j = mapSize - 1; j >= 0; j--)
                {
                    if (labelsMap[i, j].Text == string.Empty)
                    {
                        for (int k = j - 1; k >= 0; k--)
                        {
                            if (labelsMap[i, k].Text != string.Empty)
                            {
                                labelsMap[i, j].Text = labelsMap[i, k].Text;
                                labelsMap[i, k].Text = string.Empty;
                                break;
                            }
                        }
                    }
                }
            }
        }

        private void повторитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Restart();
        }

        private void выходToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit(); 
        }

        private void правилаИгрыToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("В начале игры открыты две плитки с цифрами 2 и 4 в любом их сочетании. Цифры можно перемещать влево-вправо, вверх и вниз (при помощи стрелок), но двигаются они не по одной, а блоком. Когда плитки с одинаковыми цифрами сталкиваются, их номинал суммируется, и таким образом формируются крупные числа.");
        }

        private void результатыToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var resultsForm = new ResultsForm();
            resultsForm.ShowDialog();   
        }

    

       
    }
}
