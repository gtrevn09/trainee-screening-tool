using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TraineeScreeningTool.Data;
using TraineeScreeningTool.Models;

namespace TraineeScreeningTool.Controllers;

public class HomeController : Controller
{
    private readonly ApplicationDbContext _context;

    public HomeController(ApplicationDbContext context)
    {
        _context = context;
    }

    // 1. DISPLAY: Gets the list of candidates for the Dashboard
    public async Task<IActionResult> Index()
    {
        var candidates = await _context.Candidates.ToListAsync();
        return View(candidates);
    }

    // 2. CREATE: Saves the "Add Candidate" modal data
    [HttpPost]
    public async Task<IActionResult> Create(Candidate candidate)
    {
        if (ModelState.IsValid)
        {
            _context.Add(candidate);
            await _context.SaveChangesAsync();
        }
        return RedirectToAction(nameof(Index));
    }

    // 3. RETRIEVE: Opens the assessment page for a specific candidate
    public async Task<IActionResult> Assess(int id)
    {
        var candidate = await _context.Candidates.FindAsync(id);
        if (candidate == null) return NotFound();
        return View(candidate);
    }

    // 4. SCORE: Placeholder for your team's scoring algorithm
    [HttpPost]
    public async Task<IActionResult> SubmitAssessment(int id, double it, double carp, double book)
    {
        var candidate = await _context.Candidates.FindAsync(id);
        if (candidate == null) return NotFound();

        // Save raw scores
        candidate.ITScore = it;
        candidate.CarpentryScore = carp;
        candidate.BookkeepingScore = book;

        // --- TEAM ALGORITHM START ---
        // Your team will replace this logic with the historical data math
        if (it > 80)
        {
            candidate.Recommendation = "Information Technology";
            candidate.ReadinessRating = "High";
            candidate.Explanation = "High aptitude for logical troubleshooting.";
        }
        else
        {
            candidate.Recommendation = "General Training";
            candidate.ReadinessRating = "Moderate";
            candidate.Explanation = "Consider baseline certifications first.";
        }
        // --- TEAM ALGORITHM END ---

        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    // 5. REPORT: Display the final recommendation and explanation
    public async Task<IActionResult> Details(int id)
    {
        var candidate = await _context.Candidates.FindAsync(id);
        if (candidate == null) return NotFound();
        return View(candidate);
    }
    [HttpPost]
    public async Task<IActionResult> Delete(int id)
    {
        var candidate = await _context.Candidates.FindAsync(id);
        if (candidate != null)
        {
            _context.Candidates.Remove(candidate);
            await _context.SaveChangesAsync();
        }
        return RedirectToAction(nameof(Index));
    }
}