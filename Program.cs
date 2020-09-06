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
            double _totalCost = _products.Sum(item => item._price);

            Dictionary<string, float> _allDisc = GetDiscount(_products);

            //display Total Costs & Best Discount available on Console
            Console.WriteLine("Total price before promotion: " + _totalCost);
            Console.WriteLine("Best Discount: " + _allDisc.Values.Max());            
            Console.WriteLine("Total price after promotion: " + (_totalCost - _allDisc.Values.Max()));
            Console.ReadLine();
        }

        private static Dictionary<string, float> GetDiscount(List<Product> _products)
        {            
            IPromotion iPromoOnA = new PromoOnA();
            IPromotion iPromoOnB = new PromoOnB();
            IPromotion iPromoOnCD = new PromoOnCD();

            Dictionary<string, float> _promo = new Dictionary<string, float>(); ;

            if (iPromoOnA._isPromoFulfilled(_products))
            {
                _promo.Add("Promo A", iPromoOnA.applypromo(_products));
            }
            if (iPromoOnB._isPromoFulfilled(_products))
            {
                _promo.Add("Promo B", iPromoOnB.applypromo(_products));
            }
            if (iPromoOnCD._isPromoFulfilled(_products))
            {
                _promo.Add("Promo C", iPromoOnCD.applypromo(_products));
            }

            return _promo;

        }

    }

    interface IPromotion
    {
        bool _isPromoFulfilled(List<Product> _product);
        float applypromo(List<Product> _product);
    }

 class PromoOnA : IPromotion
    {
        public string _promoDesc = "Promo A";
        int _countA = 0;
        float _costA = 0;
        public float _discountedPrice = 130;
        float _discountOnUnit = 3;

        public bool _isPromoFulfilled(List<Product> _products)
        {
            bool _isPromoFulfilled = false;

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
            }
            if (_countA >= 3)
            {
                _isPromoFulfilled = true;
            }
            return _isPromoFulfilled;
        }

        public float applypromo(List<Product> _product)
        {
            float _discPriceDiff = 0;
            Console.WriteLine("Checking Promo: " + _promoDesc + "...");
            _discPriceDiff = _costA * _discountOnUnit - _discountedPrice;
            _discPriceDiff = _discPriceDiff * Convert.ToInt32(_countA / _discountOnUnit);

            return _discPriceDiff;
        }
    }

    class PromoOnB : IPromotion
    {
        int _countB = 0;
        float _costB = 0;
        public string _promoDesc = "Promo B";
        public float _discountedPrice = 45;
        public float _discountOnUnit = 2;

        public bool _isPromoFulfilled(List<Product> _products)
        {
            bool _isPromoFulfilled = false;

            foreach (Product p in _products)
            {
                if (p._id == "B")
                {
                    _countB++;
                    if (_costB == 0)
                    {
                        _costB = p._price;
                    }
                }
            }
            if (_countB >= 2)
            {
                _isPromoFulfilled = true;
            }
            return _isPromoFulfilled;
        }

        public float applypromo(List<Product> _product)
        {
            float _discPriceDiff = 0;
            Console.WriteLine("Checking Promo: " + _promoDesc + "...");

            _discPriceDiff = _costB * _discountOnUnit - _discountedPrice;
            _discPriceDiff = _discPriceDiff * Convert.ToInt32(_countB / _discountOnUnit);

            return _discPriceDiff;
        }
    }

    class PromoOnCD : IPromotion
    {
        public string _promoDesc = "Promo C & D";
        public float _discountedPrice = 30;
        public float _discountOnUnitC = 1;
        public float _discountOnUnitD = 1;
        int _countC = 0;
        int _countD = 0;
        float _costC = 0;
        float _costD = 0;

        public bool _isPromoFulfilled(List<Product> _products)
        {
            bool _isPromoFulfilled = false;


            foreach (Product p in _products)
            {
                if (p._id == "C")
                {
                    _countC++;
                    if (_costC == 0)
                    {
                        _costC = p._price;
                    }
                }
                else if (p._id == "D")
                {
                    _countD++;
                    if (_costD == 0)
                    {
                        _costD = p._price;
                    }
                }
            }


            if (_countC > 0 && _countD > 0)
            {
                _isPromoFulfilled = true;
            }
            return _isPromoFulfilled;
        }

        public float applypromo(List<Product> _product)
        {
            float _discPriceDiff = 0;
            Console.WriteLine("Checking Promo: " + _promoDesc + "...");

            _discPriceDiff = (_costC * _discountOnUnitC + _costD * _discountOnUnitD - _discountedPrice);

            if (Convert.ToInt32(_countD / _discountOnUnitD) >= Convert.ToInt32(_countC / _discountOnUnitC))
            {
                _discPriceDiff = Convert.ToInt32(_countC / _discountOnUnitC) * _discPriceDiff;
            }
            else
            {
                _discPriceDiff = Convert.ToInt32(_countD / _discountOnUnitD) * _discPriceDiff;
            }

            return _discPriceDiff;
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
