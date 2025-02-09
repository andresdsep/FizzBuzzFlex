import { FormEvent, useState } from 'react';
import { useMutation } from 'react-query';
import { v4 as uuid } from 'uuid';
import { MatchWriteDto, RoundResponseDto } from '../models/matchDtos';
import { startMatch } from '../utils/apiHelpers';
import TextField from './TextField';

const getInitialModel = (): Omit<MatchWriteDto, 'gameId'> => ({
  id: uuid(),
  durationInSeconds: 60,
  minimumNumber: 1,
  maximumNumber: 100,
});

interface Props {
  gameId: string;
  onMatchStarted: (roundResponse: RoundResponseDto) => void;
}

const MatchParametersForm = ({ gameId, onMatchStarted }: Props) => {
  const [matchModel, setMatchModel] = useState(getInitialModel);

  const { mutate, isLoading } = useMutation(startMatch, {
    onSuccess: (data) => onMatchStarted(data),
  });
  const handleSubmit = (e: FormEvent) => {
    e.preventDefault();
    mutate({ ...matchModel, gameId });
  };

  return isLoading ? (
    <h2>Starting match...</h2>
  ) : (
    <form onSubmit={handleSubmit}>
      <TextField
        name="durationInSeconds"
        label="Game Duration (seconds)"
        model={matchModel}
        setModel={setMatchModel}
        type="number"
      />
      <TextField
        name="minimumNumber"
        label="Minimum Number"
        model={matchModel}
        setModel={setMatchModel}
        type="number"
      />
      <TextField
        name="maximumNumber"
        label="Maximum Number"
        model={matchModel}
        setModel={setMatchModel}
        type="number"
      />
      <button type="submit">Start Game</button>
    </form>
  );
};

export default MatchParametersForm;
