import { IncedentStatusEnum } from "./emums/incident-status.emum";

export interface Incedent {
    id: string;
    videoArchiveId: string;
    observationSiteId: string;
    data: Date;
    status: IncedentStatusEnum;
  }