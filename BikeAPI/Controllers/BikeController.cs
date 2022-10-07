using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BikeAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class BikeController : ControllerBase
    {
        private static List<Bike> bikes = new List<Bike>()
        {
            //  new Bike {
            //      Id = 1,
            //      Make = "Yamaha",
            //      Model = "R125",
            //      EngineSize = "125cc"
            // }
        };
        private readonly DataContext _context;

        public BikeController(DataContext context)
        {
            this._context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<Bike>>> Get()
        {
            //  return Ok(bikes);
            return Ok(await _context.Bikes.ToListAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Bike>> GetSingle(int id)
        {
            // var bike = bikes.Find(bike => bike.Id == id);
            var bike = await _context.Bikes.FindAsync(id);
            if (bike == null)
            {
                return BadRequest("Bike not found");
            }
            else
                return Ok(bike);

        }

        [HttpPost]
        public async Task<ActionResult<List<Bike>>> Add(Bike bike)
        {
            // bikes.Add(bike);
            _context.Bikes.Add(bike);
            await _context.SaveChangesAsync();
            // return Ok(bikes);
            return Ok(await _context.Bikes.ToListAsync());
        }

        [HttpPut]
        public async Task<ActionResult<List<Bike>>> Update(Bike req)
        {
            // var bike = bikes.Find(bike => bike.Id == req.Id);

            var bike = await _context.Bikes.FindAsync(req.Id);

            if (bike == null)
            {
                return BadRequest("Bike not found");
            }
            else
            {
                bike.Model = req.Model;
                bike.Make = req.Make;
                bike.EngineSize = req.EngineSize;


                await _context.SaveChangesAsync();
                // return Ok(bike);
                return Ok(await _context.Bikes.ToListAsync());
            }
        }

        [HttpDelete("id")]
        public async Task<ActionResult<List<Bike>>> Delete(int id)
        {
            // var bike = bikes.Find(bike => bike.Id == id);
            var bike = await _context.Bikes.FindAsync(id);

            if (bike == null)
            {
                return BadRequest("Bike not found");
            }
            else
            {
                // bikes.Remove(bike);
                _context.Bikes.Remove(bike);
                await _context.SaveChangesAsync();

                return Ok(await _context.Bikes.ToListAsync());
            }
        }
    }
}
