import { useState } from 'react';
import TextField from './TextField';
import { GameWriteDto } from '../models/dtos';
import { v4 as uuid } from 'uuid';

const getInitialModel = (): GameWriteDto => ({
  id: uuid(),
  name: '',
  author: '',
  divisorLabels: [
    { id: uuid(), divisor: '', label: '' },
    { id: uuid(), divisor: '', label: '' },
    { id: uuid(), divisor: '', label: '' },
  ],
});

const CreateNewGame = () => {
  const [gameModel, setGameModel] = useState(getInitialModel);

  return (
    <div>
      <h2>Create New Game</h2>
      <form>
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
