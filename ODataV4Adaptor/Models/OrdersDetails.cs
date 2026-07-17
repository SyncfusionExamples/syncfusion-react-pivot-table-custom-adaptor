using System.ComponentModel.DataAnnotations;

namespace ODataV4Adaptor.Models
{
    public class OrdersDetails
    {
        public static List<OrdersDetails> order = new List<OrdersDetails>();
        public OrdersDetails()
        {

        }
        public OrdersDetails(
        int OrderID, string CustomerId, int EmployeeId, double Freight)
        {
            this.OrderID = OrderID;
            this.CustomerID = CustomerId;
            this.EmployeeID = EmployeeId;
            this.Freight = Freight;
        }

        public static List<OrdersDetails> GetAllRecords()
        {
            if (order.Count() == 0)
            {
                int code = 10000;
                for (int i = 1; i < 2; i++)
                {
                    order.Add(new OrdersDetails(code + 1, "ALFKI", i + 0, 10.0));
                    order.Add(new OrdersDetails(code + 2, "ANATR", i + 2, 20.0));
                    order.Add(new OrdersDetails(code + 3, "ANTON", i + 1, 30.0));
                    order.Add(new OrdersDetails(code + 4, "BLONP", i + 3, 40.0));
                    order.Add(new OrdersDetails(code + 5, "BOLID", i + 4, 50.0));
                    code += 5;
                }
            }
            return order;
        }
        [Key]
        public int? OrderID { get; set; }
        public string? CustomerID { get; set; }
        public int? EmployeeID { get; set; }
        public double? Freight { get; set; }
    }
}