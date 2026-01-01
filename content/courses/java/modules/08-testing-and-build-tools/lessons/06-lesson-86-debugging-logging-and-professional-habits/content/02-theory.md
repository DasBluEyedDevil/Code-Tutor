---
type: "THEORY"
title: "Logging with SLF4J and Logback"
---

LOGGING = Structured way to record what's happening

import org.slf4j.Logger;
import org.slf4j.LoggerFactory;

public class OrderService {
    private static final Logger logger = 
        LoggerFactory.getLogger(OrderService.class);
    
    public void processOrder(Order order) {
        logger.info("Processing order: {}", order.getId());
        
        try {
            calculateTotal(order);
            saveToDatabase(order);
            logger.info("Order {} processed successfully", order.getId());
        } catch (Exception e) {
            logger.error("Failed to process order {}", order.getId(), e);
        }
    }
}

LOGGING LEVELS (in order):
TRACE - Very detailed, rarely used
DEBUG - Development information
INFO - General information ("Server started")
WARN - Something unexpected, but not critical
ERROR - Something failed

Production: Only show WARN and ERROR
Development: Show DEBUG and above