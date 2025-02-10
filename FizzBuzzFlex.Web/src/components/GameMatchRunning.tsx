import { FormEvent, useState } from 'react';
import { useMutation } from 'react-query';
import { RoundAnswerDto, RoundResponseDto } from '../models/matchDtos';
import { playRound } from '../utils/apiHelpers';
import TextField from './TextField';

interface Props {
  initialRound: RoundResponseDto;
}

const GameMatchRunning = ({ initialRound }: Props) => {
  const [currentRound, setCurrentRound] = useState(initialRound);
  const [answerModel, setAnswerModel] = useState<RoundAnswerDto>({
    matchId: currentRound.matchId,
    promptId: currentRound.promptId,
    answer: '',
  });

  const { mutate, isLoading } = useMutation(playRound, {
    onSuccess: (data) => {
      setCurrentRound(data);
      setAnswerModel({
        matchId: data.matchId,
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
      {currentRound.previousRoundResult !== null && (
        <p>
          Your previous answer was{' '}
          {currentRound.previousRoundResult ? 'correct' : 'incorrect'}!
        </p>
      )}
      <h3>Round {currentRound.roundNumber}</h3>
      <h1>{currentRound.promptNumber}</h1>
      <form onSubmit={handleSubmit}>
        <TextField
          name="answer"
          label="Answer"
          model={answerModel}
          setModel={setAnswerModel}
        />
      </form>
    </div>
  );
};

export default GameMatchRunning;
