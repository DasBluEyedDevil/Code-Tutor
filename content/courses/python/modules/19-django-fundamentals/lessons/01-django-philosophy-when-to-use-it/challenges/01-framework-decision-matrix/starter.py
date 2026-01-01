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
    team_size: int  # 1-10
    deadline_weeks: int

@dataclass
class Recommendation:
    framework: Framework
    confidence: float  # 0.0 to 1.0
    reasons: List[str]

def recommend_framework(reqs: ProjectRequirements) -> Recommendation:
    """
    Analyze requirements and recommend the best framework.
    
    Django scores high when:
    - needs_admin is True (+30 points)
    - needs_auth is True (+20 points)
    - has_complex_db is True (+25 points)
    - team_size > 3 (+15 points)
    - deadline_weeks < 8 (+20 points)
    
    FastAPI scores high when:
    - is_api_only is True (+35 points)
    - needs_async is True (+30 points)
    
    Flask scores for small projects:
    - team_size <= 2 and not needs_admin (+25 points)
    """
    # TODO: Implement scoring logic
    # Calculate scores for each framework
    # Return the highest scoring with confidence and reasons
    pass

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