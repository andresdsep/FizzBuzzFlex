/* eslint-disable @typescript-eslint/no-explicit-any */
import { produce } from 'immer';
import { InputHTMLAttributes } from 'react';

function setNestedProperty(obj: any, path: string, value: any) {
  const keys = path.split(/[\\.\\[\]\\'\\"]/).filter((key) => key);
  keys.reduce((acc, key, i) => {
    if (i === keys.length - 1) {
      acc[key] = value;
    }
    return acc[key];
  }, obj);
}

type Props<T> = Pick<InputHTMLAttributes<any>, 'autoFocus' | 'autoComplete'> & {
  name: string;
  label: string;
  model: T;
  setModel: (value: React.SetStateAction<T>) => void;
  type?: 'text' | 'number';
};

function TextField<T extends object>({
  name,
  label,
  model,
  setModel,
  type = 'text',
  ...rest
}: Props<T>) {
  return (
    <div>
      <label htmlFor={name}>{label}</label>
      <input
        type={type}
        id={name}
        name={name}
        value={(model as any)[name]}
        onChange={(e) => {
          const newModel = produce(model, (m) => {
            const newValue =
              type === 'number'
                ? Number.parseInt(e.target.value)
                : e.target.value;
            setNestedProperty(m, name, newValue);
          });
          setModel(newModel);
        }}
        {...rest}
      />
    </div>
  );
}

export default TextField;
