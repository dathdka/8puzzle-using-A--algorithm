using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AKT
{
    public class DiChuyen
    {
        public int G;
        public int H;
        public int F;
        public string TrangThai;
        public ToaDo X;
        public DiChuyen()
        {
            
        }
        public DiChuyen(int g, int h, ToaDo x,string trangthai)
        {
            G = g;
            H = h;
            F = G + H;
            X = x;
            TrangThai = trangthai;
        }

    }
}
