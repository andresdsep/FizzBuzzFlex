import axios from 'axios';
import { GameMinimalDto, GameReadDto, GameWriteDto } from '../models/gameDtos';
import {
  MatchResultsDto,
  MatchWriteDto,
  RoundAnswerDto,
  RoundResponseDto,
} from '../models/matchDtos';
import { getReactQueryConfig } from './getReactQueryConfig';

const axiosClient = axios.create({
  baseURL: import.meta.env.VITE_API_URL,
  timeout: 10000,
});

export const createNewGame = async (game: GameWriteDto) => {
  await axiosClient.post('/v1/games', game);
};

export const GetAllGamesKey = 'getAllGames';
export const getAllGames = getReactQueryConfig(
  () => async () => {
    const response = await axiosClient.get<GameMinimalDto[]>('/v1/games/all');
    return response.data;
  },
  GetAllGamesKey
);

export const GetGameKey = 'getGame';
export const getGame = getReactQueryConfig(
  (gameId: string) => async () => {
    const response = await axiosClient.get<GameReadDto>(`/v1/games/${gameId}`);
    return response.data;
  },
  GetGameKey
);

export const startMatch = async (dto: MatchWriteDto) => {
  const response = await axiosClient.post<RoundResponseDto>(
    `/v1/matches/start`,
    dto
  );
  return response.data;
};

export const playRound = async (roundAnswer: RoundAnswerDto) => {
  const response = await axiosClient.post<RoundResponseDto>(
    `/v1/matches/play-round`,
    roundAnswer
  );
  return response.data;
};

export const GetMatchResultsKey = 'getMatchResults';
export const getMatchResults = getReactQueryConfig(
  (matchId: string) => async () => {
    const response = await axiosClient.get<MatchResultsDto>(
      `/v1/matches/${matchId}/results`
    );
    return response.data;
  },
  GetMatchResultsKey,
  { staleTime: Infinity }
);
