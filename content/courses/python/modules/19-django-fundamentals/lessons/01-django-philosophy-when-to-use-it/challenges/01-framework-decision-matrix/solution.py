from dataclasses import dataclass
from typing import List
from enum import Enum

class Framework(Enum):
    DJANGO = "Django"
    FASTAPI = "FastAPI"
    FLASK = "Flask"

@dataclass
class ProjectRequirements:
    needs_admin: bool
    needs_auth: bool
    is_api_only: bool
    needs_async: bool
    has_complex_db: bool
    team_size: int
    deadline_weeks: int

@dataclass
class Recommendation:
    framework: Framework
    confidence: float
    reasons: List[str]

def recommend_framework(reqs: ProjectRequirements) -> Recommendation:
    scores = {Framework.DJANGO: 0, Framework.FASTAPI: 0, Framework.FLASK: 0}
    reasons = {Framework.DJANGO: [], Framework.FASTAPI: [], Framework.FLASK: []}
    
    # Django scoring
    if reqs.needs_admin:
        scores[Framework.DJANGO] += 30
        reasons[Framework.DJANGO].append("Built-in admin interface saves weeks of development")
    if reqs.needs_auth:
        scores[Framework.DJANGO] += 20
        reasons[Framework.DJANGO].append("Complete authentication system included")
    if reqs.has_complex_db:
        scores[Framework.DJANGO] += 25
        reasons[Framework.DJANGO].append("Powerful ORM handles complex relationships")
    if reqs.team_size > 3:
        scores[Framework.DJANGO] += 15
        reasons[Framework.DJANGO].append("Conventions help larger teams stay consistent")
    if reqs.deadline_weeks < 8:
        scores[Framework.DJANGO] += 20
        reasons[Framework.DJANGO].append("Batteries-included approach speeds up development")
    
    # FastAPI scoring
    if reqs.is_api_only:
        scores[Framework.FASTAPI] += 35
        reasons[Framework.FASTAPI].append("Optimized for API development with auto-docs")
    if reqs.needs_async:
        scores[Framework.FASTAPI] += 30
        reasons[Framework.FASTAPI].append("Native async/await support for high concurrency")
    
    # Flask scoring
    if reqs.team_size <= 2 and not reqs.needs_admin:
        scores[Framework.FLASK] += 25
        reasons[Framework.FLASK].append("Lightweight and flexible for small teams")
    
    # Find winner
    max_score = max(scores.values())
    winner = max(scores, key=scores.get)
    
    # Calculate confidence (normalize to 0-1)
    total_possible = 110  # Max possible Django score
    confidence = min(max_score / total_possible, 1.0)
    
    return Recommendation(
        framework=winner,
        confidence=confidence,
        reasons=reasons[winner] if reasons[winner] else ["Default choice for general web development"]
    )

# Test with finance tracker requirements
finance_tracker = ProjectRequirements(
    needs_admin=True,
    needs_auth=True,
    is_api_only=False,
    needs_async=False,
    has_complex_db=True,
    team_size=2,
    deadline_weeks=6
)

result = recommend_framework(finance_tracker)
print(f"Recommended: {result.framework.value}")
print(f"Confidence: {result.confidence:.0%}")
print("Reasons:")
for reason in result.reasons:
    print(f"  - {reason}")