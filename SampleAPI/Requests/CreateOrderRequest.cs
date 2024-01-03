using System.ComponentModel.DataAnnotations;

namespace SampleAPI.Requests
{
    /// <summary>
    /// Create order request class
    /// </summary>
    public class CreateOrderRequest
    {
        [MaxLength(100)]
        public string? Name { get; set; }
        [MaxLength(100)]
        public string? Description { get; set; }
    }
}
