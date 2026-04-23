using Microsoft.EntityFrameworkCore;

namespace AuctionService.controllers
{
    using AuctionService.Data;
    using AuctionService.DTOs;
    using AuctionService.Entities;
    using AutoMapper;
    using Microsoft.AspNetCore.Mvc;
    using System;
    using System.Threading.Tasks;

    
    [ApiController]
    public class AuctionController : ControllerBase
    {
        private readonly AuctionDBContext _dbContext;
        private readonly IMapper _mapper;

        public AuctionController(IMapper mapper, AuctionDBContext dbContext)
        {
            _mapper = mapper;
            _dbContext = dbContext;
        }

        [HttpPost("create")]
        public async Task<ActionResult<AuctionsDTO>> CreateAuction(CreateAuctionDto request)
        {

            // if (request == null || string.IsNullOrEmpty(request.ItemName) || request.StartingBid <= 0)
            // {
            //     return BadRequest("Invalid auction creation request.");
            // }
            var auction = _mapper.Map<Auction>(request);
            auction.Seller = "TestSeller";
            _dbContext.Auctions.Add(auction);
            var result = await _dbContext.SaveChangesAsync();
            return Ok(result);
        }

        // [HttpPost("{auctionId}/bid")]
        // public async Task<IActionResult> PlaceBid(Guid auctionId, [FromBody] BidRequest request)
        // {
        //     if (request == null || request.BidAmount <= 0)
        //     {
        //         return BadRequest("Invalid bid request.");
        //     }

        //     var result = await _auctionService.PlaceBidAsync(auctionId, request.BidderName, request.BidAmount);
        //     if (!result)
        //     {
        //         return BadRequest("Failed to place bid. Please check the auction status and bid amount.");
        //     }

        //     return Ok("Bid placed successfully.");
        // }

        [HttpGet("api/auctions/{auctionId}")]
        public async Task<ActionResult<AuctionsDTO>> GetAuctionDetails(Guid auctionId)
        {
            var auctionDetails = await _dbContext.Auctions
                .Include(x => x.Item)
                .FirstOrDefaultAsync(a => a.Id == auctionId);
            if (auctionDetails == null)
            {
                return NotFound("Auction not found.");
            }
            var result = _mapper.Map<AuctionsDTO>(auctionDetails);

            return Ok(result);
        }

        [HttpGet("api/auctions")]
        public async Task<ActionResult<List<AuctionsDTO>>> GetAllAuctions()
        {
            var auctions = await _dbContext.Auctions
                .Include(x => x.Item)
                .OrderBy(a => a.Item.Make)
                .ToListAsync();
            var result = _mapper.Map<List<AuctionsDTO>>(auctions);
            return Ok(result);
        }
    }
}