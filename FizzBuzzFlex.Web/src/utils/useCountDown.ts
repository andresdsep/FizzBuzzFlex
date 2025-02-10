import { useEffect, useState } from 'react';

const useCountDown = (initialTime: number, onFinished: () => void) => {
  const [time, setTime] = useState(initialTime);

  useEffect(() => {
    const timerRef = setInterval(() => {
      setTime((prevTime) => prevTime - 1);
    }, 1000);

    return () => {
      if (timerRef) {
        clearInterval(timerRef);
      }
    };
  }, []);
  useEffect(() => {
    if (time === 0) onFinished();
  }, [onFinished, time]);

  return { time };
};

export default useCountDown;
