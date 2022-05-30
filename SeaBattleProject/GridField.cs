﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SeaBattleProject
{
    class GridField
    {
        public int Height;
        public int Width;
        public DataGridView Dgv;
        public DataGridView DgvMove;
        public int[,] field;
        public int[,] end;
        public int[,] move;
        public Button button;


        public GridField(int height, int width, DataGridView dgv)
        {
            Height = height;
            Width = width;
            Dgv = dgv;
            field = new int[Height, Width];
            end = new int[Height, Width];
            move = new int[8, 4];
        }

        public GridField(int height, int width, DataGridView dgv, DataGridView dgvMove)
        {
            Height = height;
            Width = width;
            Dgv = dgv;
            DgvMove = dgvMove;
            field = new int[Height, Width];
            end = new int[Height, Width];
            move = new int[8, 4];
        }
        public void LoadLevel()
        {
            StreamReader file1 = new StreamReader("D:/ПМ 2021/Роза/SeaBattleProject-master/SeaBattleProject/files/field2.txt");
            StreamReader file2 = new StreamReader("D:/ПМ 2021/Роза/SeaBattleProject-master/SeaBattleProject/files/EndGame2.txt");
            switch (Height)
            {
                case 4:
                    file1 = new StreamReader("D:/ПМ 2021/Роза/SeaBattleProject-master/SeaBattleProject/files/field1.txt");
                    file2 = new StreamReader("D:/ПМ 2021/Роза/SeaBattleProject-master/SeaBattleProject/files/EndGame1.txt");
                    break;
                case 5:
                    file1 = new StreamReader("D:/ПМ 2021/Роза/SeaBattleProject-master/SeaBattleProject/files/field2.txt");
                    file2 = new StreamReader("D:/ПМ 2021/Роза/SeaBattleProject-master/SeaBattleProject/files/EndGame2.txt");
                    break;
                case 6:
                    file1 = new StreamReader("D:/ПМ 2021/Роза/SeaBattleProject-master/SeaBattleProject/files/field3.txt");
                    file2 = new StreamReader("D:/ПМ 2021/Роза/SeaBattleProject-master/SeaBattleProject/files/EndGame3.txt");
                    break;
            }
            for (int i = 0; i < Height; i++)
            {
                string[] s1 = file1.ReadLine().Split();
                string[] s2 = file2.ReadLine().Split();
                for (int j = 0; j < Width; j++)
                {
                    field[i, j] = int.Parse(s1[j]);
                    end[i, j] = int.Parse(s2[j]);
                }
            }
            file1.Close();
            file2.Close();
        }
        public void ColorTheField()
        {
            Dgv.ClearSelection();
            Dgv.Enabled = false;

            for (int i = 0; i < Height; i++)
            {
                for (int j = 0; j < Width; j++)
                {
                    switch (Height)
                    {
                        case 4:
                            if (field[i, j] == 1)
                                Dgv.Rows[i].Cells[j].Style.BackColor = Color.Yellow;

                            else if (field[i, j] == 2)
                                Dgv.Rows[i].Cells[j].Style.BackColor = Color.Violet;

                            else Dgv.Rows[i].Cells[j].Style.BackColor = Color.White;
                            break;

                        case 5:
                            if (field[i, j] == 1)
                                Dgv.Rows[i].Cells[j].Style.BackColor = Color.DarkMagenta;

                            else if (field[i, j] == 2)
                                Dgv.Rows[i].Cells[j].Style.BackColor = Color.DeepPink;

                            else if (field[i, j] == 3)
                                Dgv.Rows[i].Cells[j].Style.BackColor = Color.BlueViolet;

                            else Dgv.Rows[i].Cells[j].Style.BackColor = Color.White;
                            break;

                        case 6:
                            if (field[i, j] == 1)
                                Dgv.Rows[i].Cells[j].Style.BackColor = Color.BlanchedAlmond;

                            else if (field[i, j] == 2)
                                Dgv.Rows[i].Cells[j].Style.BackColor = Color.DarkOrchid;

                            else if (field[i, j] == 3)
                                Dgv.Rows[i].Cells[j].Style.BackColor = Color.Indigo;

                            else if (field[i, j] == 4)
                                Dgv.Rows[i].Cells[j].Style.BackColor = Color.HotPink;

                            else Dgv.Rows[i].Cells[j].Style.BackColor = Color.White;
                            break;
                    }

                }
            }
        }

        public void Click(object sender, EventArgs e)
        {
            button = (Button)sender;
            if (DgvMove.CurrentCell.Value == null)
            {
                DgvMove.CurrentCell.Value = button.Image;
                move[DgvMove.CurrentCell.RowIndex, DgvMove.CurrentCell.ColumnIndex] =
                    Convert.ToInt32(button.Tag.ToString());

            }
            DgvMove.CurrentCell.Selected = false;
        }
        public bool TryStep(int StepShip, int y, int x, int[,] tempField)
        {
            if (StepShip == 4 && y - 1 >= 0)
            {
                tempField[x, y - 1] = field[x, y];
                return true;
            }

            if (StepShip == 3 && y + 1 < Height)
            {
                tempField[x, y + 1] = field[x, y];
                return true;
            }

            if (StepShip == 2 && x + 1 < Width)
            {
                tempField[x + 1, y] = field[x, y];
                return true;
            }

            if (StepShip == 1 && x - 1 >= 0)
            {
                tempField[x - 1, y] = field[x, y];
                return true;
            }
            return false;
        }

        public void ToRun(int stepIndex)
        {
            var tempField = new int[Height, Width];
            bool win = true;
            for (int i = 0; i < stepIndex; i++)
            {
                win = true;
                var step1 = move[i, 0];
                var step2 = move[i, 1];
                var step3 = move[i, 2];
                var step4 = move[i, 3];
                

                for (int x = 0; x < Height; x++)
                {
                    for (int y = 0; y < Width; y++)
                    {

                        if (field[x, y] == 1 && step1 != 0)
                            win = TryStep(step1, y, x, tempField);

                        else if (field[x, y] == 2 && step2 != 0)
                            win = TryStep(step2, y, x, tempField);

                        else if (field[x, y] == 3 && step3 != 0)
                            win = TryStep(step3, y, x, tempField);

                        else if (field[x, y] == 4 && step4 != 0)
                            win = TryStep(step4, y, x, tempField);

                        if (win == false && step1+step2+step3+step4>0)
                            goto finish;

                    }
                }
                field = tempField;
                ColorTheField();
            }

            finish:
            ColorTheField();
            if (win && CheckWin()) MessageBox.Show("Поздравляю, вы выиграли!");
            else MessageBox.Show("Вы проиграли");
        }

        public bool CheckWin()
        {
            for (int i = 0; i < Height; i++)
            {
                for (int j = 0; j < Width; j++)
                {
                    if (field[i, j] != end[i, j])
                    {
                        ColorTheField();
                        return false;
                    }
                }
            }
            return true;
        }

        public void Restart(int stepindex)
        {
            for (int i = 0; i < stepindex; i++)
            {
                switch (Height)
                {
                    case 4:
                        for (int j = 0; j <2; j++)
                        {
                            DgvMove.Rows[i].Cells[j].Value = null;
                        }
                        break;
                    case 5:
                        for (int j = 0; j < 3; j++)
                        {
                            DgvMove.Rows[i].Cells[j].Value = null;
                        }
                        break;
                    case 6:
                        for (int j = 0; j < 4; j++)
                        {
                            DgvMove.Rows[i].Cells[j].Value = null;
                        }
                        break;
                }
            }
        }

    }
}

