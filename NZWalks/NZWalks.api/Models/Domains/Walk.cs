namespace NZWalks.api.Models.Domains
{
    public class Walk
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public double Length { get; set; } 

        public Guid RegionId { get; set; }

        public Guid WalkDiffcultyId { get; set; }

        // Navigation property

        public Region Region { get; set; }
        public WalkDiffculty walkDiffculty { get; set; }    
    }
}
