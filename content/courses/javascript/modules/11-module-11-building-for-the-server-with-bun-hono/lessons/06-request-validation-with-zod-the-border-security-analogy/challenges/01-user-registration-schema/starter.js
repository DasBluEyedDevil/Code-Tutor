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

// TODO: Create registration schema
const registrationSchema = z.object({
  // Define your fields here
});

// Test data
const validUser = {
  email: 'user@example.com',
  password: 'SecurePass123',
  confirmPassword: 'SecurePass123',
  username: 'johndoe',
  age: 25
};

const invalidUser = {
  email: 'invalid-email',
  password: 'weak',
  confirmPassword: 'different',
  username: 'ab',  // too short
  age: 10  // too young
};

// TODO: Test both and print results
