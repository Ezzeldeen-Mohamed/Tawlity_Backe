namespace Tawlity_Backend.Dtos
{
    public class FavoriteDto
    {
        public int Id { get; set; }
        public string? RestaurantName { get; set; }
        public string? MenuItemName { get; set; }
        public DateTime AddedOn { get; set; }
    }
}
