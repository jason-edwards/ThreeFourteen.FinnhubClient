using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ThreeFourteen.Finnhub.Client.Limits
{
    public class ApiRequest
    {
        /// Primary key for table - maps with unique Symbol + Echange
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long RequestId { get; set; }

        [MaxLength(50)]
        public string Description { get; set; }

        public DateTime RequestTime { get; set; }

        public long Weight { get; set; }
    }
}
