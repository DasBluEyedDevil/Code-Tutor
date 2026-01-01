import React, { useMemo, useEffect } from 'react';

interface PasswordRequirement {
  id: string;
  label: string;
  met: boolean;
}

interface PasswordStrengthMeterProps {
  password: string;
  onStrengthChange?: (isStrong: boolean) => void;
}

const calculateStrength = (password: string): number => {
  let score = 0;
  if (password.length >= 8) score++;
  if (/[a-z]/.test(password) && /[A-Z]/.test(password)) score++;
  if (/\d/.test(password)) score++;
  if (/[!@#$%^&*(),.?":{}|<>]/.test(password)) score++;
  return score;
};

const checkRequirements = (password: string): PasswordRequirement[] => {
  return [
    {
      id: 'length',
      label: 'At least 8 characters',
      met: password.length >= 8
    },
    {
      id: 'case',
      label: 'Uppercase and lowercase letters',
      met: /[a-z]/.test(password) && /[A-Z]/.test(password)
    },
    {
      id: 'number',
      label: 'At least one number',
      met: /\d/.test(password)
    },
    {
      id: 'special',
      label: 'At least one special character (!@#$%^&*)',
      met: /[!@#$%^&*(),.?":{}|<>]/.test(password)
    }
  ];
};

const getStrengthColor = (strength: number): string => {
  switch (strength) {
    case 0:
    case 1:
      return '#dc2626'; // red
    case 2:
      return '#f97316'; // orange
    case 3:
      return '#eab308'; // yellow
    case 4:
      return '#22c55e'; // green
    default:
      return '#dc2626';
  }
};

const getStrengthLabel = (strength: number): string => {
  switch (strength) {
    case 0:
      return 'Very Weak';
    case 1:
      return 'Weak';
    case 2:
      return 'Fair';
    case 3:
      return 'Good';
    case 4:
      return 'Strong';
    default:
      return '';
  }
};

const PasswordStrengthMeter: React.FC<PasswordStrengthMeterProps> = ({
  password,
  onStrengthChange
}) => {
  const strength = useMemo(() => calculateStrength(password), [password]);
  const requirements = useMemo(() => checkRequirements(password), [password]);
  const color = useMemo(() => getStrengthColor(strength), [strength]);
  const label = useMemo(() => getStrengthLabel(strength), [strength]);
  const isStrong = strength >= 4;

  useEffect(() => {
    onStrengthChange?.(isStrong);
  }, [isStrong, onStrengthChange]);

  if (!password) return null;

  return (
    <div className="password-strength-meter">
      <div className="strength-bar-container">
        <div
          className="strength-bar"
          style={{
            width: `${(strength / 4) * 100}%`,
            backgroundColor: color,
            height: '8px',
            borderRadius: '4px',
            transition: 'width 0.3s, background-color 0.3s'
          }}
        />
      </div>
      
      <div className="strength-label" style={{ color, fontWeight: 'bold' }}>
        {label}
      </div>
      
      <ul className="requirements-list" style={{ listStyle: 'none', padding: 0 }}>
        {requirements.map(req => (
          <li
            key={req.id}
            style={{
              color: req.met ? '#22c55e' : '#6b7280',
              display: 'flex',
              alignItems: 'center',
              gap: '8px'
            }}
          >
            <span>{req.met ? '\u2713' : '\u2717'}</span>
            <span>{req.label}</span>
          </li>
        ))}
      </ul>
      
      {isStrong && (
        <div className="success-message" style={{ color: '#22c55e' }}>
          Strong password!
        </div>
      )}
    </div>
  );
};

export default PasswordStrengthMeter;