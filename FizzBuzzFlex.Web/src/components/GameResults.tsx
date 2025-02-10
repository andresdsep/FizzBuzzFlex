import { useQuery } from 'react-query';
import { getMatchResults } from '../utils/apiHelpers';

interface Props {
  matchId: string;
  onReset: () => void;
}

const GameResults = ({ matchId, onReset }: Props) => {
  const { data: results, isLoading } = useQuery(getMatchResults([matchId]));
  return isLoading ? (
    <h2>Loading match results...</h2>
  ) : !results ? (
    <h2>Results not foud</h2>
  ) : (
    <div>
      <h1>Time's up!</h1>
      <h3>
        You got {results.correctAnswers} answers right, and{' '}
        {results.incorrectAnswers} wrong.
      </h3>
      <h3>
        {results.correctAnswers > results.incorrectAnswers
          ? 'Good job!'
          : 'Better luck next time.'}
      </h3>
      <button onClick={onReset}>Back to game screen</button>
    </div>
  );
};

export default GameResults;
