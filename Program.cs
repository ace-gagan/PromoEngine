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

            // Get product details from console
            Console.WriteLine("Total number of products");
            int _totalOrder = Convert.ToInt32(Console.ReadLine());

            for (int i = 0; i < _totalOrder; i++)
            {
                Console.WriteLine("Enter the type of the product: A/B/C/D");
                string _prodType = Console.ReadLine().ToUpper();
                Product p = new Product(_prodType);
                _products.Add(p);
            }

            //get total cost before discount
            float _totalCost = _products.Sum(item => item._price);

            CalculatePrice _cp = new CalculatePrice(_products);

            //get  details of all applicable promos
            Dictionary<string, float> _allDisc = _cp.GetDiscount();

            //display Total Costs & Best Discount available on Console
            Console.WriteLine("Total price before promotion: " + _totalCost);
            Console.WriteLine("Best Discount: " + _allDisc.Values.Max());
            Console.WriteLine("Applying promotion...");
            Console.WriteLine("Total price after promotion: " + (_totalCost - _allDisc.Values.Max()));
            Console.ReadLine();
        }
    }

    public interface IPromotion
    {
        bool _isPromoFulfilled(List<Product> _product);
        float ApplyPromo_GetDiscount(List<Product> _product);
        //string getPromoDesc();

    }

    public class PromoOnSKU : IPromotion
    {
        string _promoDesc = "Promo on 'n' items of an SKU";
        int _countSKU = 0;
        float _costSKU = 0;
        public float _discountedPrice = 0;
        float _discountOnUnit = 0;
        string _promoOnID = "";

        public PromoOnSKU(string id, float discountedPrice, float discountOnUnit)
        {
            _promoOnID = id;
            _discountedPrice = discountedPrice;
            _discountOnUnit = discountOnUnit;
        }

        public bool _isPromoFulfilled(List<Product> _products)
        {
            bool _isPromoFulfilled = false;

            foreach (Product p in _products)
            {
                if (p._id == _promoOnID)
                {
                    _countSKU++;
                    if (_costSKU == 0)
                    {
                        _costSKU = p._price;
                    }
                }
            }
            if (_countSKU >= _discountOnUnit)
            {
                _isPromoFulfilled = true;
            }
            return _isPromoFulfilled;
        }

        public float ApplyPromo_GetDiscount(List<Product> _product)
        {
            float _discPriceDiff = 0;
            Console.WriteLine("Checking Promo on " + _promoOnID + ": " + _promoDesc + "...");

            _discPriceDiff = _costSKU * _discountOnUnit - _discountedPrice;
            _discPriceDiff = _discPriceDiff * (Int32)(_countSKU / _discountOnUnit);

            return _discPriceDiff;
        }
    }

    public class PromoOnSKU1SKU2 : IPromotion
    {
        public string _promoDesc = "Promo on 'x' items of SKU1 & 'y' items of SKU2";
        public float _discountedPrice = 30;
        public float _discountOnUnitSKU1 = 1;
        public float _discountOnUnitSKU2 = 1;
        int _countSKU1 = 0;
        int _countSKU2 = 0;
        float _costSKU1 = 0;
        float _costSKU2 = 0;
        string _promoOnID1 = "";
        string _promoOnID2 = "";

        public PromoOnSKU1SKU2(string idSKU1, string idSKU2, float discountOnUnitSKU1, float discountOnUnitSKU2, float discountedPrice)
        {
            _promoOnID1 = idSKU1;
            _promoOnID2 = idSKU2;

            _discountOnUnitSKU1 = discountOnUnitSKU1;
            _discountOnUnitSKU2 = discountOnUnitSKU2;

            _discountedPrice = discountedPrice;

        }

        public bool _isPromoFulfilled(List<Product> _products)
        {
            bool _isPromoFulfilled = false;

            foreach (Product p in _products)
            {
                if (p._id == _promoOnID1)
                {
                    _countSKU1++;
                    if (_costSKU1 == 0)
                    {
                        _costSKU1 = p._price;
                    }
                }
                else if (p._id == _promoOnID2)
                {
                    _countSKU2++;
                    if (_costSKU2 == 0)
                    {
                        _costSKU2 = p._price;
                    }
                }
            }

            if (_countSKU1 > 0 && _countSKU2 > 0)
            {
                _isPromoFulfilled = true;
            }
            return _isPromoFulfilled;
        }

        public float ApplyPromo_GetDiscount(List<Product> _product)
        {
            float _discPriceDiff = 0;
            Console.WriteLine("Checking Promo on " + _promoOnID1 + "/" + _promoOnID2 + ": " + _promoDesc + "...");

            _discPriceDiff = (_costSKU1 * _discountOnUnitSKU1 + _costSKU2 * _discountOnUnitSKU2 - _discountedPrice);

            if ((Int32)(_countSKU2 / _discountOnUnitSKU2) >= (Int32)(_countSKU1 / _discountOnUnitSKU1))
            {
                _discPriceDiff = (Int32)(_countSKU1 / _discountOnUnitSKU1) * _discPriceDiff;
            }
            else
            {
                _discPriceDiff = (Int32)(_countSKU2 / _discountOnUnitSKU2) * _discPriceDiff;
            }

            return _discPriceDiff;
        }
    }

    public class CalculatePrice
    {
        List<Product> _products = new List<Product>();

        public CalculatePrice(List<Product> p)
        {
            _products = p;
        }

        /// <summary>
        /// Returns a Dictionary of all available Discounts
        /// </summary>
        /// <param name="_products"></param>
        /// <returns></returns>
        public Dictionary<string, float> GetDiscount()
        {
            //defining avtive Promos
            IPromotion iPromoOnA = new PromoOnSKU("A", 130, 3);
            IPromotion iPromoOnB = new PromoOnSKU("B", 45, 2);
            IPromotion iPromoOnCD = new PromoOnSKU1SKU2("C", "D", 1, 1, 30);

            Dictionary<string, float> _promo = new Dictionary<string, float>(); ;

            if (iPromoOnA._isPromoFulfilled(_products))
            {
                _promo.Add("Promo A", iPromoOnA.ApplyPromo_GetDiscount(_products));
            }
            if (iPromoOnB._isPromoFulfilled(_products))
            {
                _promo.Add("Promo B", iPromoOnB.ApplyPromo_GetDiscount(_products));
            }
            if (iPromoOnCD._isPromoFulfilled(_products))
            {
                _promo.Add("Promo C & D", iPromoOnCD.ApplyPromo_GetDiscount(_products));
            }

            return _promo;
        }
    }

    public class Product
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
