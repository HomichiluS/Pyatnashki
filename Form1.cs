namespace Pyatnashki
{
    public partial class Form1 : Form
    {
        private const int Rows = 3;
        private const int Cols = 5;
        private Button[,] buttons = new Button[Rows, Cols];
        private Point emptySpot;

        public Form1()
        {

            InitializeComponent();
            InitializeGame();
            ShuffleButtons();
        }

        private void InitializeGame()
        {
            this.ClientSize = new Size(560, 400);
            this.Text = "Пятнашки";

            int buttonSize = 100;
            int spacing = 10;

            for (int i = 0; i < Rows; i++)
            {
                for (int j = 0; j < Cols; j++)
                {
                    buttons[i, j] = new Button
                    {
                        Size = new Size(buttonSize, buttonSize),
                        Location = new Point(
                            spacing + j * (buttonSize + spacing),
                            spacing + i * (buttonSize + spacing)),
                        Tag = new Point(i, j),
                        Font = new Font("Arial", 14)
                    };
                    buttons[i, j].Click += Button_Click;
                    this.Controls.Add(buttons[i, j]);
                }
            }
            emptySpot = new Point(Rows - 1, Cols - 1);
            buttons[emptySpot.X, emptySpot.Y].Visible = false;
        }

        private void ShuffleButtons()
        {
            Random rand = new Random();
            List<int> numbers = new List<int>();

            for (int i = 1; i <= Rows * Cols - 1; i++)
                numbers.Add(i);

            for (int i = 0; i < Rows; i++)
            {
                for (int j = 0; j < Cols; j++)
                {
                    if (i == emptySpot.X && j == emptySpot.Y) continue;
                    buttons[i, j].Text = numbers.Count > 0 ? numbers[rand.Next(numbers.Count)].ToString() : "";
                    numbers.Remove(int.Parse(buttons[i, j].Text));
                }
            }
        }

        private void Button_Click(object sender, EventArgs e)
        {
            Button clickedButton = (Button)sender;
            Point position = (Point)clickedButton.Tag;

            if (IsAdjacent(position, emptySpot))
            {
                SwapButtons(position, emptySpot);
                emptySpot = position;
                CheckWin();
            }
        }

        private bool IsAdjacent(Point a, Point b)
        {
            return (Math.Abs(a.X - b.X) == 1 && a.Y == b.Y) ||
                   (Math.Abs(a.Y - b.Y) == 1 && a.X == b.X);
        }

        private void SwapButtons(Point a, Point b)
        {
            buttons[a.X, a.Y].Visible = false;
            buttons[b.X, b.Y].Text = buttons[a.X, a.Y].Text;
            buttons[b.X, b.Y].Visible = true;
            buttons[a.X, a.Y].Text = "";
        }

        private void CheckWin()
        {
            int counter = 1;
            for (int i = 0; i < Rows; i++)
            {
                for (int j = 0; j < Cols; j++)
                {
                    if (i == emptySpot.X && j == emptySpot.Y && counter == Rows * Cols)
                        continue;

                    if (buttons[i, j].Text != counter.ToString())
                        return;

                    counter++;
                }
            }
            MessageBox.Show("Вы победили!");
        }
    }
}