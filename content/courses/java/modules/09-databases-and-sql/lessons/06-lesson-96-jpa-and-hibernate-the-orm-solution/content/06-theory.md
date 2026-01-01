---
type: "THEORY"
title: "Relationships - Connecting Entities"
---

@Entity
public class Student {
    @Id
    @GeneratedValue(strategy = GenerationType.IDENTITY)
    private Long id;
    private String name;
    
    @OneToMany(mappedBy = "student", cascade = CascadeType.ALL)
    private List<Enrollment> enrollments = new ArrayList<>();
}

@Entity
public class Enrollment {
    @Id
    @GeneratedValue(strategy = GenerationType.IDENTITY)
    private Long id;
    
    @ManyToOne
    @JoinColumn(name = "student_id")
    private Student student;
    
    @ManyToOne
    @JoinColumn(name = "course_id")
    private Course course;
}

RELATIONSHIP TYPES:
@OneToOne - User has one Profile
@OneToMany - Student has many Enrollments
@ManyToOne - Enrollment belongs to one Student
@ManyToMany - Students have many Courses, Courses have many Students