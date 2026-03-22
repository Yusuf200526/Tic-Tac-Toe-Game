using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using XO_Game_Abo_Hahdoud_Solution.Properties;

namespace XO_Game_Abo_Hahdoud_Solution
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        stGameStatus GameStatus;
        enPlayerTurn PlayerTurn;
        enum enWinner
        {
            Player1,
            Player2,
            Draw,
            GameInProgress
        };

        enum enPlayerTurn
        {
            Player1,
            Player2
        };

        struct stGameStatus
        {
            public enWinner Winner;
            public bool GameOver;
            public short PlayCount;
        }

        
        private void EndGame()
        {
            lblTurn.Text = "Game Over";

            switch(GameStatus.Winner)
            {
                case enWinner.Player1:
                    lblWinner.Text = "Player1";
                    break;
                case enWinner.Player2:
                    lblWinner.Text = "Player2";
                    break;
                default:
                    lblWinner.Text = "Draw";
                    break;
            }

            MessageBox.Show("GameOver", "GameOver", MessageBoxButtons.OK, MessageBoxIcon.Information);

            button1.Enabled = false;
            button2.Enabled = false;
            button3.Enabled = false;
            button4.Enabled = false;
            button5.Enabled = false;
            button6.Enabled = false;
            button7.Enabled = false;
            button8.Enabled = false;
            button9.Enabled = false;

        }
        private bool CheckValues(Button btn1,Button btn2,Button btn3)
        {
            if(btn1.Tag!="?"&&btn1.Tag==btn2.Tag&&btn2.Tag==btn3.Tag)
            {
                btn1.BackColor = Color.GreenYellow;
                btn2.BackColor = Color.GreenYellow;
                btn3.BackColor = Color.GreenYellow;

                if(btn1.Tag=="X")
                {
                    GameStatus.Winner = enWinner.Player1; 
                    GameStatus.GameOver = true;
                    EndGame();
                    return true;
                }
                else
                {
                    GameStatus.Winner = enWinner.Player2;
                    GameStatus.GameOver = true;
                    EndGame();
                    return true;
                }


            }

            GameStatus.GameOver = false;
            return false;
        }
        private void CheckWinner()
        {
            if (CheckValues(button1, button2, button3))
                return;

            if (CheckValues(button4, button5, button6))
                return;

            if (CheckValues(button7, button8, button9))
                return;

            if (CheckValues(button1, button4, button7))
                return;

            if (CheckValues(button2, button5, button8))
                return;

            if (CheckValues(button3, button6, button9))
                return;

            if (CheckValues(button1, button5, button9))
                return;

            if (CheckValues(button3, button5, button7))
                return;

        }


        private void ChangeImage(Button btn)
        {


            if (btn.Tag == "?")
            {
                switch (PlayerTurn)
                {
                    case enPlayerTurn.Player1:
                        btn.Image = Resources.X;
                        PlayerTurn = enPlayerTurn.Player2;
                        GameStatus.PlayCount++;
                        lblTurn.Text = "Player2";
                        btn.Tag = "X";
                        CheckWinner();
                        break;
                    case enPlayerTurn.Player2:
                        btn.Image = Resources.O;
                        PlayerTurn = enPlayerTurn.Player1;
                        GameStatus.PlayCount++;
                        lblTurn.Text = "Player1";
                        btn.Tag = "O";
                        CheckWinner();
                        break;
                }


            }
            else
            {
                MessageBox.Show("Wrong Choice", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            if(GameStatus.PlayCount==9)
            {
                GameStatus.Winner = enWinner.Draw;
                GameStatus.GameOver = true;
                EndGame();
            }
        }
        // One Evern for clicking instead of 9 ones. 
        private void button_Click(object sender, EventArgs e)
        {
            ChangeImage((Button)sender);
        }
       

        private void ResetButtons(Button btn)
        {
            btn.Enabled = true;
            btn.Image = Resources.question_mark_961;
            btn.Tag = "?";
            btn.BackColor = Color.Transparent;
        }
        private void btnRestart_Click(object sender, EventArgs e)
        {
            ResetButtons(button1);
            ResetButtons(button2);
            ResetButtons(button3);
            ResetButtons(button4);
            ResetButtons(button5);
            ResetButtons(button6);
            ResetButtons(button7);
            ResetButtons(button8);
            ResetButtons(button9);

            GameStatus.Winner = enWinner.GameInProgress;
            GameStatus.GameOver = false;
            GameStatus.PlayCount = 0;
            PlayerTurn = enPlayerTurn.Player1;

            lblTurn.Text = "Player 1";
            lblWinner.Text = "In Progress";

        }
    }
}
