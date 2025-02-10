import { GameReadDto } from '../models/gameDtos';
import { MatchSettings } from '../models/helperTypes';
import MatchParametersForm from './MatchParametersForm';

interface Props {
  game: GameReadDto;
  onMatchStarted: (matchSettings: MatchSettings) => void;
}

const GameIntroduction = ({ game, onMatchStarted }: Props) => (
  <div>
    <h1>Welcome to {game.name}!</h1>
    <h3>Created by {game.author}.</h3>
    <p>
      Over a series of rounds, you'll be given a number prompt. For each number:
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
    <MatchParametersForm gameId={game.id} onMatchStarted={onMatchStarted} />
  </div>
);

export default GameIntroduction;
