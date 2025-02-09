import { useParams } from 'react-router-dom';

const PlayPage = () => {
  const { gameId } = useParams<{ gameId: string }>();
  return (
    <div>
      <h1>Play Page</h1>
      <p>Game ID: {gameId}</p>
    </div>
  );
};

export default PlayPage;
