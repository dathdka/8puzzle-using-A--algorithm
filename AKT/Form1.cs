using System;
using System.Collections.Generic;
using System.Windows.Forms;
namespace AKT
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
           
        }

        private void Form1_Load(object sender, EventArgs e)
        {
        }


        public void button23_Click(object sender, EventArgs e)
        {
            int[,] S = new int[3, 3];
            S[0, 0] = int.Parse(textBox1.Text.ToString());
            S[0, 1] = int.Parse(textBox2.Text.ToString());
            S[0, 2] = int.Parse(textBox3.Text.ToString());
            S[1, 0] = int.Parse(textBox4.Text.ToString());
            S[1, 1] = int.Parse(textBox5.Text.ToString());
            S[1, 2] = int.Parse(textBox6.Text.ToString());
            S[2, 0] = int.Parse(textBox8.Text.ToString());
            S[2, 1] = int.Parse(textBox9.Text.ToString());
            S[2, 2] = int.Parse(textBox10.Text.ToString());
            /*{
                { 2,     8,      3},
                { 1,     6,      4},
                { 7,     0,      5}   
            };*/
            int[,] E = new int[3, 3]
            {
                {1,      2,      3},
                {8,      0,      4},
                {7,      6,      5},
            };
            int H = KiemTra(S, E);
            int G = 0;
            ToaDo OTrong = new ToaDo();
            Stack<AKT.DiChuyen> QuaTrinh = new Stack<AKT.DiChuyen>();
            DiChuyen first = new DiChuyen(G, H, OTrong, "N");
            QuaTrinh.Push(first);
            while(QuaTrinh.Count >0)
            {
                bool flag = false;
                var move = QuaTrinh.Pop();
                for (int i = 0; i < 3; i++)
                    {
                        for (int j = 0; j < 3; j++)
                        {
                            if (S[i, j]  == 0)
                            {
                                OTrong.x = i;
                                OTrong.y = j;
                                OTrong.GiaTri = S[i, j];
                                DiChuyen RealMove = DiChuyen(OTrong, S, E, move.G, move.H );
                                S[i, j] = S[RealMove.X.x, RealMove.X.y];
                                S[RealMove.X.x, RealMove.X.y] = 0;
                                txtKetQua.Text += RealMove.TrangThai + "->";
                                if (RealMove.H==0)
                                {
                                    MessageBox.Show("Thành công");
                                    return;
                                }
                                QuaTrinh.Push(RealMove);
                                flag = true;
                                break;
                            }
                        }
                        if (flag)
                            break;
                    }
                G++;
            }
        }

        //sao chép mảng
        public void CopyArr (int[,] TempArr, int[,] S)
        {
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    TempArr[i, j] = S[i, j];
                }
            }
        }
        //Di chuyển mảng
        public DiChuyen DiChuyen (ToaDo OTrong, int[,] S,int[,] E, int G, int H)
        {
            int HTemp = 0;
            var TempArr = new int[3, 3];
            CopyArr(TempArr, S);
            DiChuyen RealMove;
            //Check đi lên
            ToaDo moi = new ToaDo();
            DiLen(OTrong, TempArr, moi);
            ToaDo Min = new ToaDo();
            swap(OTrong, moi, TempArr);
            HTemp = KiemTra(TempArr, E);
            RealMove = new DiChuyen(G + 1, HTemp, moi, "Lên");
            Min.x = moi.x;
            Min.y = moi.y;
            Min.GiaTri = moi.GiaTri;

            //Check đi xuống
            CopyArr(TempArr, S);
            DiXuong(OTrong, TempArr, moi);
            swap(OTrong, moi, TempArr);
            HTemp = KiemTra(TempArr, E);
            if (HTemp < RealMove.H)
            {
                RealMove.H = HTemp;
                Min.x = moi.x;
                Min.y = moi.y;
                Min.GiaTri = moi.GiaTri;
                RealMove.TrangThai = "Xuống";
            }

            //Check sang phải
            CopyArr(TempArr, S);
            SangPhai(OTrong, TempArr, moi);
            swap(OTrong, moi, TempArr);
            HTemp = KiemTra(TempArr, E);
            if (HTemp < RealMove.H)
            {
                RealMove.H = HTemp;
                Min.x = moi.x;
                Min.y = moi.y;
                Min.GiaTri = moi.GiaTri;
                RealMove.TrangThai = "Phải";
            }

            //Check sang trái
            CopyArr(TempArr, S);
            SangTrai(OTrong, TempArr, moi);
            swap(OTrong, moi, TempArr);
            HTemp = KiemTra(TempArr, E);
            if (HTemp < RealMove.H)
            {
                RealMove.H = HTemp;
                Min.x = moi.x;
                Min.y = moi.y;
                Min.GiaTri = moi.GiaTri;
                RealMove.TrangThai = "Trái";
            }
            RealMove.X = Min;
            return RealMove;
        }

        //tính H2
        public int KiemTra(int[,] S, int[,] E)
        {
            int H = 0;
            Stack<ToaDo> khac = new Stack<ToaDo>();
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (S[i,j] != E[i,j] && S[i, j] !=0)
                    {
                        ToaDo x = new ToaDo(i, j, S[i, j]);
                        khac.Push(x);
                    }
                }
            }
            while (khac.Count > 0)
            {
                ToaDo x = khac.Pop();
                for (int i = 0; i < 3; i++)
                {
                    for (int j = 0; j < 3; j++)
                    {
                        if (x.GiaTri == E[i,j])
                        {
                            H += (Math.Abs(x.x - i) + Math.Abs(x.y - j));
                            break;
                        }
                    }
                }
            }
            return H;
        }

        //đổi chỗ phần tử
        public void swap (ToaDo a, ToaDo b, int[,] TempArr)
        {
            TempArr[a.x, a.y] = b.GiaTri;
            TempArr[b.x, b.y] = 0;
        }

        public void DiLen( ToaDo a, int[,] S, ToaDo moi)
        {
            moi.y = a.y;
            if (a.x - 1 >=0 )
            {
                moi.x = a.x - 1;
                moi.GiaTri = S[a.x-1, a.y];
            }
            else
            {
                moi.x = a.x;
                moi.GiaTri = S[a.x, a.y];
            }
        }
        public void DiXuong(ToaDo a, int[,] S, ToaDo moi)
        {
            moi.y = a.y;
            if (a.x + 1 <3 )
            {
                moi.x = a.x + 1;
                moi.GiaTri = S[a.x+1 , a.y];
            }
            else
            {
                moi.x = a.x;
                moi.GiaTri = S[a.x, a.y];
            }

        }
        public void SangTrai(ToaDo a, int[,] S, ToaDo moi)
        {
            moi.x = a.x;
            if (a.y - 1 >=0 )
            {
                moi.y = a.y - 1;
                moi.GiaTri = S[a.x, a.y - 1];
            }
            else
            {
                moi.y = a.y;
                moi.GiaTri = S[a.x, a.y];
            }
        }
        public void SangPhai(ToaDo a, int[,] S, ToaDo moi)
        {
            moi.x = a.x;
            if (a.y + 1 <3 )
            {
                moi.y = a.y + 1;
                moi.GiaTri = S[a.x, a.y + 1];
            }
            else
            {
                moi.y = a.y;
                moi.GiaTri = S[a.x, a.y];
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            txtKetQua.Text = "";
        }
    }
}
