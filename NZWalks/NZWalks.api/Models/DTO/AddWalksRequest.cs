﻿namespace NZWalks.api.Models.DTO
{
    public class AddWalksRequest
    {

        public string Name { get; set; }
        public double Length { get; set; }

        public Guid RegionId { get; set; }

        public Guid WalkDiffcultyId { get; set; }
    }
}
