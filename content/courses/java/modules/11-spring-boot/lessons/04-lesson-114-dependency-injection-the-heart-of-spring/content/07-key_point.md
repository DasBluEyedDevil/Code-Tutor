---
type: "KEY_POINT"
title: "When @Autowired IS Still Needed"
---

Only when you have MULTIPLE constructors:

@Service
public class FlexibleService {
    private final UserService userService;
    private final Optional<CacheService> cacheService;
    
    // Tell Spring to use THIS constructor
    @Autowired
    public FlexibleService(UserService userService,
                          @Autowired(required = false) CacheService cache) {
        this.userService = userService;
        this.cacheService = Optional.ofNullable(cache);
    }
    
    // Alternative constructor for testing
    public FlexibleService(UserService userService) {
        this(userService, null);
    }
}

BEAN SCOPES:
By default, beans are Singleton (one instance):

@Service  // Singleton by default
public class UserService { }

Other scopes:
@Service
@Scope("prototype")  // New instance every time
public class PrototypeService { }

Most of the time, Singleton is what you want!