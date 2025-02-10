import { FormEvent, useState } from 'react';
import { useMutation } from 'react-query';
import { MatchSettings } from '../models/helperTypes';
import { RoundAnswerDto, RoundResponseDto } from '../models/matchDtos';
import { playRound } from '../utils/apiHelpers';
import useCountDown from '../utils/useCountDown';
import TextField from './TextField';

interface Props {
  matchSettings: MatchSettings;
  onMatchEnded: (matchId: string) => void;
}

const GameMatchRunning = ({ matchSettings, onMatchEnded }: Props) => {
  const [currentRound, setCurrentRound] = useState<RoundResponseDto>({
    roundNumber: matchSettings.roundNumber,
    promptId: matchSettings.promptId,
    promptNumber: matchSettings.promptNumber,
    previousRoundResult: undefined,
  });
  const [answerModel, setAnswerModel] = useState<RoundAnswerDto>({
    matchId: matchSettings.matchId,
    promptId: currentRound.promptId,
    answer: '',
  });

  const { time } = useCountDown(matchSettings.durationInSeconds, () =>
    onMatchEnded(matchSettings.matchId)
  );

  const { mutate, isLoading } = useMutation(playRound, {
    onSuccess: (data) => {
      setCurrentRound(data);
      setAnswerModel({
        matchId: matchSettings.matchId,
        promptId: data.promptId,
        answer: '',
      });
    },
  });

  const handleSubmit = (event: FormEvent<HTMLFormElement>): void => {
    event.preventDefault();
    mutate(answerModel);
  };

  return isLoading ? (
    <h2>Checking answer...</h2>
  ) : (
    <div>
      {currentRound.previousRoundResult !== undefined && (
        <p>
          Your previous answer was{' '}
          {currentRound.previousRoundResult ? 'correct' : 'incorrect'}!
        </p>
      )}
      <h2>Round {currentRound.roundNumber}</h2>
      <h1>{currentRound.promptNumber}</h1>
      <form onSubmit={handleSubmit}>
        <TextField
          name="answer"
          label="Answer"
          model={answerModel}
          setModel={setAnswerModel}
          autoFocus
          autoComplete="off"
        />
        <button>Submit</button>
      </form>
      <h3>{time} seconds remaining</h3>
    </div>
  );
};

export default GameMatchRunning;
