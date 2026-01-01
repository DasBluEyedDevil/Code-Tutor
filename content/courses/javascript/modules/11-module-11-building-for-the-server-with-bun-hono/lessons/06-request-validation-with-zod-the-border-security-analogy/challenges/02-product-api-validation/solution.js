// Enhanced Zod simulation with arrays and coercion
const z = {
  string: () => ({ _type:'string', _checks:[], _optional:false,
    min: function(n,m) { this._checks.push({type:'min',val:n,msg:m}); return this; },
    max: function(n,m) { this._checks.push({type:'max',val:n,msg:m}); return this; },
    optional: function() { this._optional = true; return this; }
  }),
  number: () => ({ _type:'number', _checks:[], _optional:false,
    positive: function(m) { this._checks.push({type:'positive',msg:m}); return this; },
    int: function() { this._checks.push({type:'int'}); return this; },
    min: function(n,m) { this._checks.push({type:'min',val:n,msg:m}); return this; },
    multipleOf: function(n) { this._checks.push({type:'multipleOf',val:n}); return this; },
    optional: function() { this._optional = true; return this; }
  }),
  boolean: () => ({ _type:'boolean', _checks:[], _optional:false,
    optional: function() { this._optional = true; return this; }
  }),
  array: (item) => ({ _type:'array', _itemSchema:item, _checks:[], _optional:false,
    min: function(n,m) { this._checks.push({type:'min',val:n,msg:m}); return this; },
    max: function(n,m) { this._checks.push({type:'max',val:n,msg:m}); return this; },
    optional: function() { this._optional = true; return this; }
  }),
  record: (val) => ({ _type:'record', _valSchema:val, _optional:false,
    optional: function() { this._optional = true; return this; }
  }),
  coerce: {
    number: () => ({ _type:'coerce_number', _checks:[], _optional:false,
      positive: function() { this._checks.push({type:'positive'}); return this; },
      optional: function() { this._optional = true; return this; }
    }),
    boolean: () => ({ _type:'coerce_boolean', _checks:[], _optional:false,
      optional: function() { this._optional = true; return this; }
    })
  },
  object: (shape) => ({
    _shape: shape, _refinements: [],
    refine: function(fn, opts) { this._refinements.push({fn,opts}); return this; },
    safeParse: function(data) {
      const errors = [];
      const output = {};
      for (const [key, schema] of Object.entries(this._shape)) {
        let val = data[key];
        if (schema._type === 'coerce_number' && val !== undefined) val = Number(val);
        if (schema._type === 'coerce_boolean' && val !== undefined) val = val === 'true' || val === true;
        if (val === undefined && schema._optional) continue;
        if (val === undefined) { errors.push({path:[key],message:`${key} is required`}); continue; }
        output[key] = val;
        if ((schema._type === 'number' || schema._type === 'coerce_number') && typeof val !== 'number')
          errors.push({path:[key],message:'Must be a number'});
        if (schema._type === 'array' && !Array.isArray(val))
          errors.push({path:[key],message:'Must be an array'});
        for (const check of schema._checks || []) {
          if (check.type === 'min') {
            const len = Array.isArray(val) ? val.length : (typeof val === 'string' ? val.length : val);
            if (len < check.val) errors.push({path:[key],message:check.msg||`Min ${check.val}`});
          }
          if (check.type === 'max') {
            const len = Array.isArray(val) ? val.length : (typeof val === 'string' ? val.length : val);
            if (len > check.val) errors.push({path:[key],message:check.msg||`Max ${check.val}`});
          }
          if (check.type === 'positive' && val <= 0) errors.push({path:[key],message:'Must be positive'});
          if (check.type === 'int' && !Number.isInteger(val)) errors.push({path:[key],message:'Must be integer'});
          if (check.type === 'multipleOf' && (val * 100) % (check.val * 100) !== 0)
            errors.push({path:[key],message:`Must have max ${Math.log10(1/check.val)} decimal places`});
        }
      }
      for (const ref of this._refinements) {
        if (!ref.fn(output)) errors.push({path:ref.opts?.path||[],message:ref.opts?.message||'Invalid'});
      }
      return errors.length ? {success:false,error:{errors}} : {success:true,data:output};
    }
  })
};

// Product schema
const productSchema = z.object({
  name: z.string().min(1, 'Name is required').max(100, 'Name too long'),
  price: z.number().positive('Price must be positive').multipleOf(0.01),
  quantity: z.number().int().min(0, 'Quantity cannot be negative'),
  categories: z.array(z.string()).min(1, 'At least 1 category').max(5, 'Maximum 5 categories'),
  metadata: z.record(z.string()).optional()
});

// Search query schema with price range validation
const searchQuerySchema = z.object({
  minPrice: z.coerce.number().positive().optional(),
  maxPrice: z.coerce.number().positive().optional(),
  category: z.string().optional(),
  inStock: z.coerce.boolean().optional()
}).refine(
  (data) => {
    if (data.minPrice !== undefined && data.maxPrice !== undefined) {
      return data.maxPrice >= data.minPrice;
    }
    return true;
  },
  { message: 'maxPrice must be >= minPrice', path: ['maxPrice'] }
);

// Helper to format errors
function formatErrors(errors) {
  return errors.map(e => `  ${e.path.join('.')}: ${e.message}`).join('\n');
}

// Test POST /products
const newProduct = {
  name: 'Wireless Mouse',
  price: 29.99,
  quantity: 100,
  categories: ['electronics', 'accessories']
};

console.log('=== POST /products ===');
const productResult = productSchema.safeParse(newProduct);
if (productResult.success) {
  console.log('Product valid:', productResult.data);
} else {
  console.log('Product invalid:\n' + formatErrors(productResult.error.errors));
}

// Test GET /products?minPrice=10&maxPrice=50&inStock=true
const queryParams = {
  minPrice: '10',
  maxPrice: '50',
  inStock: 'true'
};

console.log('\n=== GET /products ===');
const queryResult = searchQuerySchema.safeParse(queryParams);
if (queryResult.success) {
  console.log('Query params valid:', queryResult.data);
  console.log('Types coerced: minPrice is', typeof queryResult.data.minPrice);
} else {
  console.log('Query params invalid:\n' + formatErrors(queryResult.error.errors));
}

// Test invalid price range
console.log('\n=== GET /products (invalid range) ===');
const badQuery = { minPrice: '100', maxPrice: '50' };
const badResult = searchQuerySchema.safeParse(badQuery);
if (!badResult.success) {
  console.log('Correctly rejected:\n' + formatErrors(badResult.error.errors));
}