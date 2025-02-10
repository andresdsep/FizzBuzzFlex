import { useState } from 'react';
import { useQuery } from 'react-query';
import { useParams } from 'react-router-dom';
import GameIntroduction from '../components/GameIntroduction';
import GameMatchRunning from '../components/GameMatchRunning';
import GameResults from '../components/GameResults';
import { MatchSettings } from '../models/helperTypes';
import { getGame } from '../utils/apiHelpers';

const PlayPage = () => {
  const { gameId } = useParams<{ gameId: string }>();
  const { data: game, isLoading } = useQuery(getGame([gameId!]));
  const [matchSettings, setMatchSettings] = useState<MatchSettings>();

  const [resultsMatchId, setResultsMatchId] = useState('');
  const onMatchEnded = (matchId: string) => {
    setResultsMatchId(matchId);
    setMatchSettings(undefined);
  };
  const onReset = () => setResultsMatchId('');

  return isLoading ? (
    <h2>Loading games...</h2>
  ) : !game ? (
    <h2>Game not found</h2>
  ) : resultsMatchId ? (
    <GameResults matchId={resultsMatchId} onReset={onReset} />
  ) : matchSettings ? (
    <GameMatchRunning
      matchSettings={matchSettings}
      onMatchEnded={onMatchEnded}
    />
  ) : (
    <GameIntroduction game={game} onMatchStarted={setMatchSettings} />
  );
};

export default PlayPage;
