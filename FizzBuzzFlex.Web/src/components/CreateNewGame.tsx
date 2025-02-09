import { FormEvent, Fragment, useState } from 'react';
import { useMutation, useQueryClient } from 'react-query';
import { v4 as uuid } from 'uuid';
import { GameWriteDto } from '../models/gameDtos';
import { createNewGame, GetAllGamesKey } from '../utils/apiHelpers';
import TextField from './TextField';

const getInitialModel = (): GameWriteDto => ({
  id: uuid(),
  name: '',
  author: '',
  divisorLabels: [
    { id: uuid(), divisor: 0, label: '' },
    { id: uuid(), divisor: 0, label: '' },
    { id: uuid(), divisor: 0, label: '' },
  ],
});

const CreateNewGame = () => {
  const [gameModel, setGameModel] = useState(getInitialModel);
  const queryClient = useQueryClient();

  const { mutate, isLoading } = useMutation(createNewGame, {
    onSuccess: () => queryClient.invalidateQueries(GetAllGamesKey),
  });
  const handleSubmit = (e: FormEvent) => {
    e.preventDefault();
    mutate(gameModel);
  };

  return isLoading ? (
    <h2>Creating game...</h2>
  ) : (
    <div>
      <h2>Create New Game</h2>
      <form onSubmit={handleSubmit}>
        <TextField
          name="name"
          label="Game Name"
          model={gameModel}
          setModel={setGameModel}
        />
        <TextField
          name="author"
          label="Author"
          model={gameModel}
          setModel={setGameModel}
        />
        {gameModel.divisorLabels.map((dl, i) => (
          <Fragment key={dl.id}>
            <TextField
              name={`divisorLabels[${i}].divisor`}
              label={`Divisor ${i + 1}`}
              model={gameModel}
              setModel={setGameModel}
              type="number"
            />
            <TextField
              name={`divisorLabels[${i}].label`}
              label={`Label ${i + 1}`}
              model={gameModel}
              setModel={setGameModel}
            />
          </Fragment>
        ))}

        <button>Create Game</button>
      </form>
    </div>
  );
};

export default CreateNewGame;
