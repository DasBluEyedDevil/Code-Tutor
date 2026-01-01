// Simulated Zod-like validation library
const z = {
  string: () => ({
    _type: 'string', _checks: [], _optional: false,
    email: function() { this._checks.push({type:'email'}); return this; },
    min: function(n,m) { this._checks.push({type:'min',val:n,msg:m}); return this; },
    max: function(n,m) { this._checks.push({type:'max',val:n,msg:m}); return this; },
    regex: function(r,m) { this._checks.push({type:'regex',val:r,msg:m}); return this; },
    optional: function() { this._optional = true; return this; }
  }),
  number: () => ({
    _type: 'number', _checks: [], _optional: false,
    int: function() { this._checks.push({type:'int'}); return this; },
    min: function(n,m) { this._checks.push({type:'min',val:n,msg:m}); return this; },
    optional: function() { this._optional = true; return this; }
  }),
  object: (shape) => ({
    _shape: shape, _refinements: [],
    refine: function(fn, opts) { this._refinements.push({fn,opts}); return this; },
    safeParse: function(data) {
      const errors = [];
      for (const [key, schema] of Object.entries(this._shape)) {
        const val = data[key];
        if (val === undefined && schema._optional) continue;
        if (val === undefined) { errors.push({path:[key],message:`${key} is required`}); continue; }
        for (const check of schema._checks || []) {
          if (check.type === 'email' && !/^[^\s@]+@[^\s@]+\.[^\s@]+$/.test(val))
            errors.push({path:[key],message:'Invalid email'});
          if (check.type === 'min' && (typeof val === 'string' ? val.length < check.val : val < check.val))
            errors.push({path:[key],message:check.msg||`Must be at least ${check.val}`});
          if (check.type === 'max' && (typeof val === 'string' ? val.length > check.val : val > check.val))
            errors.push({path:[key],message:check.msg||`Must be at most ${check.val}`});
          if (check.type === 'regex' && !check.val.test(val))
            errors.push({path:[key],message:check.msg||'Invalid format'});
          if (check.type === 'int' && !Number.isInteger(val))
            errors.push({path:[key],message:'Must be an integer'});
        }
        if (schema._type === 'number' && typeof val !== 'number')
          errors.push({path:[key],message:'Must be a number'});
      }
      for (const ref of this._refinements) {
        if (!ref.fn(data)) errors.push({path:ref.opts.path||[],message:ref.opts.message});
      }
      return errors.length ? {success:false,error:{errors}} : {success:true,data};
    }
  })
};

// Registration schema with all validations
const registrationSchema = z.object({
  email: z.string().email(),
  password: z.string()
    .min(8, 'Password must be at least 8 characters')
    .regex(/[A-Z]/, 'Password must contain uppercase letter')
    .regex(/[a-z]/, 'Password must contain lowercase letter')
    .regex(/[0-9]/, 'Password must contain a number'),
  confirmPassword: z.string(),
  username: z.string()
    .min(3, 'Username must be at least 3 characters')
    .max(20, 'Username must be at most 20 characters')
    .regex(/^[a-zA-Z0-9]+$/, 'Username must be alphanumeric'),
  age: z.number().int().min(13, 'Must be at least 13 years old').optional()
}).refine(
  (data) => data.password === data.confirmPassword,
  { message: 'Passwords do not match', path: ['confirmPassword'] }
);

// Helper to format errors nicely
function formatErrors(errors) {
  return errors.map(e => `  ${e.path.join('.')}: ${e.message}`).join('\n');
}

// Test valid user
const validUser = {
  email: 'user@example.com',
  password: 'SecurePass123',
  confirmPassword: 'SecurePass123',
  username: 'johndoe',
  age: 25
};

console.log('=== Testing Valid User ===');
const validResult = registrationSchema.safeParse(validUser);
if (validResult.success) {
  console.log('Validation passed!');
  console.log('User data:', validResult.data);
} else {
  console.log('Validation failed!');
}

// Test invalid user
const invalidUser = {
  email: 'invalid-email',
  password: 'weak',
  confirmPassword: 'different',
  username: 'ab',
  age: 10
};

console.log('\n=== Testing Invalid User ===');
const invalidResult = registrationSchema.safeParse(invalidUser);
if (invalidResult.success) {
  console.log('Validation passed!');
} else {
  console.log('Validation failed! Errors:');
  console.log(formatErrors(invalidResult.error.errors));
}