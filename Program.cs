using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PromoEngine
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Product> _products = new List<Product>();

            Console.WriteLine("Total number of products");
            int _totalOrder = Convert.ToInt32(Console.ReadLine());

            for (int i = 0; i < _totalOrder; i++)
            {
                Console.WriteLine("Enter the type of the product: A/B/C/D");
                string _prodType = Console.ReadLine().ToUpper();
                Product p = new Product(_prodType);
                _products.Add(p);
            }

            float _totalPrice = GetTotalPrice(_products);
            Console.WriteLine("Total price after promotion: " + _totalPrice);
            Console.ReadLine();
        }

        private static float GetTotalPrice(List<Product> _products)
        {
            int _countA = 0;
            int _countB = 0;
            int _countC = 0;
            int _countD = 0;
            float _costA = 0;
            float _costB = 0;
            float _costC = 0;
            float _costD = 0;

            foreach (Product p in _products)
            {
                if (p._id == "A")
                {
                    _countA++;
                    if (_costA == 0)
                    {
                        _costA = p._price;
                    }
                }
                if (p._id == "B")
                {
                    _countB++;
                    if (_costB == 0)
                    {
                        _costB = p._price;
                    }
                }
                if (p._id == "C")
                {
                    _countC++;
                    if (_costC == 0)
                    {
                        _costC = p._price;
                    }
                }
                if (p._id == "D")
                {
                    _countD++;
                    if (_costD == 0)
                    {
                        _costD = p._price;
                    }
                }
            }

            float _totalCostA = (_countA / 3) * 130 + (_countA % 3) * _costA;
            float _totalCostB = (_countB / 2) * 45 + (_countB % 2) * _costB;
            float _totalCostC_D = 0;

            if (_countC > 0 && _countD > 0)
            {
                if (_countC >= _countD)
                {
                    _totalCostC_D = _countD * 30 + (_countC - _countD) * _costC;
                }
                else
                {
                    _totalCostC_D = _countC * 30 + (_countD - _countC) * _costD;
                }
            }
            else
            {
                _totalCostC_D = _countC * _costC + _countC * _countD;
            }

            return _totalCostA + _totalCostB + _totalCostC_D;
        }

    }

    class Product
    {
        public string _id { get; set; }
        public float _price { get; set; }

        public Product(string id)
        {
            this._id = id;
            switch (id)
            {
                case "A":
                    this._price = 50;
                    break;
                case "B":
                    this._price = 30;
                    break;
                case "C":
                    this._price = 20;
                    break;
                case "D":
                    this._price = 15;
                    break;
            }
        }
    }

}
