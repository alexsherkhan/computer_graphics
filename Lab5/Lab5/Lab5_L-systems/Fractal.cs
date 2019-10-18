using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Lab5_L_systems
{
    public class Fractal
    {
        string init = "";
        int startX, startY;
        public double startLength = 200.0;
        public int startAngle;
        string sourse = "";
        char start;
        int angle;
        int curAngle;
        int additionalAngle = -1;
        int x, y;
        int width = 1;
        double length;
        Color color = Color.FromArgb(255, 140, 70, 20);
        int generations = 0;
        HashSet<char> drawForward = new HashSet<char>();
        Dictionary<char, string> rules = new Dictionary<char, string>();
        Stack<Store> storage = new Stack<Store>();
        public List<MyPoint> l = new List<MyPoint>();

        public int maxX, maxY;
        public int minX, minY;

        public Fractal(string filename)
        {
            maxX = minX = startX;
            maxY = minY = startY;
            x = startX;
            y = startY;
            length = startLength;

            StreamReader sr = new StreamReader(filename);
            string str;
            string[] a = sr.ReadLine().Split(' ');
            foreach (string s in a)
            {
                if (s.Length > 0)
                {
                    drawForward.Add(s[0]);
                }
            }
            str = sr.ReadLine();
            string[] arr = str.Split(' ');
            sourse = arr[0];
            init = sourse;
            start = arr[0][0];
            angle = int.Parse(arr[1]);
            startAngle = int.Parse(arr[2]);
            if (arr.Length > 3)
            {
                additionalAngle = int.Parse(arr[3]);
                color = Color.FromArgb(51, 25, 0);
            }
            while (true)
            {
                str = sr.ReadLine();
                if (str == null)
                    break;
                arr = str.Split(' ');
                rules.Add(arr[0][0], arr[2]);
            }
            sr.Close();
        }

        public void Reset()
        {
            width = 1;
            color = Color.FromArgb(140, 70, 20);
            maxX = minX = startX;
            maxY = minY = startY;
            generations = 0;
            x = startX;
            y = startY;
            length = startLength;
            sourse = init;
            curAngle = startAngle;
            storage.Clear();
            l.Clear();
        }

        public void Iterate()
        {
            generations++;
            string temp = "";
            for (int i = 0; i < sourse.Length; ++i)
            {
                if (rules.ContainsKey(sourse[i]))
                {
                    temp += rules[sourse[i]];
                }
                else
                {
                    temp += sourse[i];
                }
            }
            sourse = temp;
        }

        public void ParseSourse()
        {
            if (additionalAngle != -1)
            {
                width = generations * 2;
            }
            if (generations == 0)
                generations = 256;
            int stepR = -140 / generations;
            int stepG = 185 / generations;
            int stepB = -20 / generations;
            int t = 0;
            Random r = new Random(DateTime.Now.Millisecond);
            for (int i = 0; i < sourse.Length; ++i)
            {
                if (drawForward.Contains(sourse[i]))
                {
                    int a = x + (int)(Math.Cos(curAngle / (180.0 / Math.PI)) * length);
                    int b = y + (int)(Math.Sin(curAngle / (180.0 / Math.PI)) * length);
                    if (minX > a)
                        minX = a;
                    else if (maxX < a)
                        maxX = a;
                    if (minY > b)
                        minY = b;
                    else if (maxY < b)
                        maxY = b;
                    l.Add(new MyPoint(x, y, a, b, curAngle, color, width));
                    x = a;
                    y = b;
                }
                else if (sourse[i] == '+')
                {
                    if (additionalAngle == -1)
                        curAngle += angle;
                    else
                        curAngle += r.Next(angle, additionalAngle);
                    if (curAngle > 360)
                        curAngle -= 360;
                }
                else if (sourse[i] == '-')
                {
                    if (additionalAngle == -1)
                        curAngle -= angle;
                    else
                        curAngle -= r.Next(angle, additionalAngle);
                    if (curAngle < 0)
                        curAngle += 360;
                }
                else if (sourse[i] == '[')
                {
                    storage.Push(new Store(x, y, curAngle, color, length, width));
                }
                else if (sourse[i] == ']')
                {
                    x = storage.Peek().x;
                    y = storage.Peek().y;
                    curAngle = storage.Peek().angle;
                    length = storage.Peek().length;
                    color = storage.Peek().color;
                    width = storage.Peek().width;
                    storage.Pop();
                    ++t;
                }
                else if (sourse[i] == '@')
                {
                    length *= 0.8;
                    color = Color.FromArgb(255, color.R + stepR, color.G + stepG, color.B + stepB);
                    width -= 2;
                }
            }
        }
    }
}
