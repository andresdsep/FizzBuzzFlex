import { useQuery } from 'react-query';
import { useParams } from 'react-router-dom';
import MatchParametersForm from '../components/MatchParametersForm';
import { getGame } from '../utils/apiHelpers';

const PlayPage = () => {
  const { gameId } = useParams<{ gameId: string }>();

  const { data: game, isLoading } = useQuery(getGame([gameId!]));

  return isLoading ? (
    <h2>Loading games...</h2>
  ) : !game ? (
    <h2>Game not found</h2>
  ) : (
    <div>
      <h1>Welcome to {game.name}!</h1>
      <h3>Created by {game.author}.</h3>
      <p>
        Over a series of rounds, you'll be given a number prompt. For each
        number:
      </p>
      <ul>
        {game.divisorLabels.map((dl) => (
          <li key={dl.id}>
            If the number is divisible by {dl.divisor}, add "{dl.label}" to your
            answer
          </li>
        ))}
        <li>
          If the number isn't divisible by any of the above, enter the prompt as
          the answer
        </li>
      </ul>
      <p>First up, let's enter some match parameters and we can get started!</p>
      <MatchParametersForm
        gameId={game.id}
        onMatchStarted={() => console.log('Not implemented')}
      />
    </div>
  );
};

export default PlayPage;
