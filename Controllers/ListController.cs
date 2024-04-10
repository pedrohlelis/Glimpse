// using Microsoft.AspNetCore.Mvc;
// using Microsoft.EntityFrameworkCore;
// using Glimpse.Migrations;

// namespace Glimpse.Controllers;

// [Route("api/[Controller]")]
// [ApiController]
// public class ListController : Controller
// {
//     private readonly GlimpseContext _db;

//     public ListController()
//     {
//         _db = new GlimpseContext());
//     }

//     [HttpGet]
//     public IActionResult Get()
//     {
//         var Lists = _db.Lists.ToList();
//         return Ok(Lists);
//     }

//     [HttpPut]
//     public IActionResult Edit(List newList)
//     {
//         List outdatedList = _db.Lists.Find(newList.ListId);
//         _db.Entry(outdatedList).CurrentValues.SetValues(newList);
//         _db.SaveChanges();
//         return Ok(newList);
//     }

//     [HttpPost]
//     public IActionResult Create(List list)
//     {
//         _db.Lists.Add(list);
//         _db.SaveChanges();
//         return Ok(list);
//     }

//     [HttpDelete]
//     public IActionResult Delete(int listId)
//     {
//         List toRemoveList = _db.Lists.Find(listId);
//         _db.Lists.Remove(toRemoveList);
//         _db.SaveChanges();
//         return Ok("Lista excluída");
//     }
// }