import { useState } from 'react';
import { useQuery } from 'react-query';
import { useParams } from 'react-router-dom';
import GameIntroduction from '../components/GameIntroduction';
import GameMatchRunning from '../components/GameMatchRunning';
import { RoundResponseDto } from '../models/matchDtos';
import { getGame } from '../utils/apiHelpers';

const PlayPage = () => {
  const { gameId } = useParams<{ gameId: string }>();
  const { data: game, isLoading } = useQuery(getGame([gameId!]));
  const [initialRound, setInitialRound] = useState<RoundResponseDto>();

  return isLoading ? (
    <h2>Loading games...</h2>
  ) : !game ? (
    <h2>Game not found</h2>
  ) : initialRound ? (
    <GameMatchRunning initialRound={initialRound} />
  ) : (
    <GameIntroduction game={game} onMatchStarted={setInitialRound} />
  );
};

export default PlayPage;
