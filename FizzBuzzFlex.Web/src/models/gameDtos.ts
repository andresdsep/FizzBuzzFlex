export interface GameWriteDto {
  id: string;
  name: string;
  author: string;
  divisorLabels: DivisorLabelWriteDto[];
}

export interface DivisorLabelWriteDto {
  id: string;
  divisor: number;
  label: string;
}

export interface GameMinimalDto {
  id: string;
  name: string;
  author: string;
  createdDate: Date;
}

export interface GameReadDto {
  id: string;
  name: string;
  author: string;
  createdDate: Date;
  divisorLabels: DivisorLabelReadDto[];
}

export interface DivisorLabelReadDto {
  id: string;
  divisor: number;
  label: string;
}
