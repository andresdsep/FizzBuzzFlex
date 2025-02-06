import { FormEvent, useState } from 'react';
import TextField from './TextField';
import { GameWriteDto } from '../models/dtos';
import { v4 as uuid } from 'uuid';
import { useMutation } from 'react-query';
import { createNewGame } from '../utils/apiHelpers';

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

  const { mutate, isLoading } = useMutation(createNewGame);
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
        {gameModel.divisorLabels.map((_, i) => (
          <>
            <TextField
              name={`divisorLabels[${i}].divisor`}
              label={`Divisor ${i + 1}`}
              model={gameModel}
              setModel={setGameModel}
            />
            <TextField
              name={`divisorLabels[${i}].label`}
              label={`Label ${i + 1}`}
              model={gameModel}
              setModel={setGameModel}
            />
          </>
        ))}

        <button>Create Game</button>
      </form>
    </div>
  );
};

export default CreateNewGame;
