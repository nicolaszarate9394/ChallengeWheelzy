using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChallengeWheelzy.QuestionNumber4
{
    public class Order
    {
        public int OrderId { get; set; }
        public int CustomerId { get; set; }
        public int StatusId { get; set; }
        public DateTime OrderDate { get; set; }
        public bool IsActive { get; set; }
    }
}
